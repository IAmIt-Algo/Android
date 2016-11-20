using Mindblower.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Mindblower.Core
{
    public class Synchronizer : IAmItRequestListener,IDisposable
    {
        public static bool IsUsed = false;
        private List<GameObject> _levelStarters { get; set; }
        public Synchronizer()
        {
            _levelStarters = GameObject.FindGameObjectsWithTag("Starter").ToList();
            _levelStarters.Sort((a, b) => ExtractNumber(a.name) - ExtractNumber(b.name));
        }
        public void Synchronize()
        {
            IAmItHttpRequest.Get<GetUserInformationModel>(IAmItServerMethods.GET_INFO, this);
            IsUsed = true;
        }
        private int ExtractNumber(string s)
        {
            return Int32.Parse(Regex.Match(s, @"\d+").Value);
        }
        public void OnFail(string code)
        {
            Debug.Log("Something go wrong. Request has returned with code " + code);
        }

        public void OnGet<T>(T responseModel)
        {
            var levels = (responseModel as GetUserInformationModel).Levels;
            levels.Sort((a, b) => ExtractNumber(a.Name) - ExtractNumber(b.Name));
            _levelStarters.ForEach((s) => { s.GetComponentsInChildren<Image>().ToList().ForEach((y) => { y.enabled = false; }); });
            var enumerator = _levelStarters.GetEnumerator();
            levels.ForEach((x) => {
                enumerator.Current.GetComponentsInChildren<Image>().Take(x.StarsCount).ToList().ForEach((y) => { y.enabled = true; });
                enumerator.MoveNext();
            });
        }

        public void OnLogin()
        {
            throw new NotImplementedException();
        }

        public void OnLogOut()
        {
            throw new NotImplementedException();
        }

        public void OnPost(string s)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                _levelStarters = null;
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Synchronizer() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
