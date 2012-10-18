using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using xKnight.Models;

namespace xKnight.Attacking
{
    internal class SimpleXssAttackerAgent : AttackerAgent
    {
        #region Fields

        XssAttackingSharedReource _sharedResource;
        private XAttack _xAttack;

        #endregion

        #region Constructors

        public SimpleXssAttackerAgent(XssAttackingSharedReource sharedResource)
        {
            _sharedResource = sharedResource;
        }

        #endregion

        #region Interface

        public override void Attack()
        {
            Form form = null;

            while ((form = GetNotVistedForm()) != null)
            {
                if (HasReflectedResults(form))
                {
                    _xAttack = new XAttack();
                    _xAttack.AttackId = _sharedResource.SharedAttack.Id;
                    _xAttack.FormId = form.Id;
                    _xAttack.StartTime = DateTime.Now;

                    XAttackParam[] attackParams= ComputeAttackParams(form);
                    for (int i = 0; i < attackParams.Length; i++)
                        _xAttack.XAttackParams.Add(attackParams[i]);

                    _xAttack.AttackContent = CreateAttackVector(attackParams, form);
                    _xAttack.ResponsePage = DoAttack(form, _xAttack.AttackContent);
                    _xAttack.FinishTime = DateTime.Now;

                    DataLayer.Save(_xAttack);     
                }
            }
        }

        public override void AttackAsync()
        {
            AttackAsyncCaller asyncCaller = new AttackAsyncCaller(Attack);

            OnAgentAttackStarted();
            asyncCaller.BeginInvoke(new AsyncCallback(OnAgentAttackCompleted), null);
        }

        #endregion

        #region Private Methods

        private Form GetNotVistedForm()
        {
            Form form = null;
            lock (_sharedResource.SharedLock)
            {
                form = _sharedResource.SharedQueue.Count != 0 ? _sharedResource.SharedQueue.Dequeue() : null;
                Console.WriteLine(String.Format("Queue : {0}, Thread : {1}", _sharedResource.SharedQueue.Count, Thread.CurrentThread.ManagedThreadId));
            }

            return form;
        }

        private string DoAttack(Form form, string attackContent)
        {
            try
            {

                SimpleXssAttackAnnounceItem announceItem = new SimpleXssAttackAnnounceItem(_xAttack, SimpleXssAttackStatus.AttackStarted, _sharedResource, "", DateTime.Now);
                OnAgentAttackAnnounced(announceItem);

                HttpWebRequest request = null;

                if (form.Method == "get")
                    request = WebRequest.Create(form.Action + attackContent) as HttpWebRequest;
                else
                    request = WebRequest.Create(form.Action) as HttpWebRequest;

                request.Timeout = 100000;
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
                request.AllowAutoRedirect = true;
                request.KeepAlive = false;

                if(form.Method=="post")
                {
                    request.ContentType = "";
                    byte[] data = Encoding.UTF8.GetBytes(attackContent);
                    request.ContentLength = data.Length;
                    
                    using(Stream stream=request.GetRequestStream())
                    {
                        stream.Write(data,0,data.Length);
                    }
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (
                        (response.StatusCode != HttpStatusCode.NotFound
                        || response.StatusCode != HttpStatusCode.BadGateway
                        || response.StatusCode != HttpStatusCode.BadRequest
                        || response.StatusCode != HttpStatusCode.Forbidden
                        || response.StatusCode != HttpStatusCode.GatewayTimeout
                        || response.StatusCode != HttpStatusCode.Gone
                        || response.StatusCode != HttpStatusCode.InternalServerError
                        || response.StatusCode != HttpStatusCode.NotAcceptable)
                        && (response.ContentType.Contains("text/html"))
                        )
                    {
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            string resp=sr.ReadToEnd();

                            _sharedResource.IncrementAttacks();
                            announceItem = new SimpleXssAttackAnnounceItem(_xAttack, SimpleXssAttackStatus.AttackFinished, _sharedResource, "", DateTime.Now);
                            OnAgentAttackAnnounced(announceItem);

                            return resp;
                        }
                    }
                    else
                    {
                        announceItem = new SimpleXssAttackAnnounceItem(_xAttack, SimpleXssAttackStatus.AttackHalted, _sharedResource, "", DateTime.Now);
                        OnAgentAttackAnnounced(announceItem);
                        return null;
                    }
                }
            }
            catch (WebException ex)
            {
                SimpleXssAttackAnnounceItem announceItem = new SimpleXssAttackAnnounceItem(_xAttack, SimpleXssAttackStatus.AttackHalted, _sharedResource, "", DateTime.Now);
                OnAgentAttackAnnounced(announceItem);
                return null;
            }
        }

        private string CreateAttackVector(XAttackParam[] attackParams, Form form)
        {
            string postData = "";
            for (int i = 0; i < form.FormElements.Count; i++)
            {
                FormElement element = form.FormElements.ElementAt(i);
                
                if(!(form.Method=="get" && element.Type=="submit"))
                    postData+=string.Format("{0}={1}&",element.Name,HttpUtility.UrlEncode(attackParams[i].Value));
            }
            
            postData = postData.Substring(0, postData.Length - 1);

            if (form.Method == "get")
                postData = "?" + postData;

            if (form.Method=="get" && !form.Action.EndsWith("/"))
                postData = "/" + postData;

            return postData;
        }

        private XAttackParam[] ComputeAttackParams(Form form)
        {
            List<XAttackParam> lstParams=new List<XAttackParam>();

            for (int i = 0; i < form.FormElements.Count; i++)
			{
			    FormElement element=form.FormElements.ElementAt(i);

                if(element.Type=="hidden")
                {
                    lstParams.Add(new XAttackParam() { Value=element.Value,FormElementId=element.Id});
                }
                else if (element.Type == "text"
                    || element.Type == "password"
                    || element.Type == "email")
                {
                    lstParams.Add(new XAttackParam() { Value = GetInjectionValue(), FormElementId = element.Id });
                }
                else
                {
                    lstParams.Add(new XAttackParam() { Value = "", FormElementId = element.Id });
                }

			}

            return lstParams.ToArray();
        }

        private string GetInjectionValue()
        {
            return "";
        }

        private bool HasReflectedResults(Form form)
        {
            return true;
        }

        #endregion
    }
}
