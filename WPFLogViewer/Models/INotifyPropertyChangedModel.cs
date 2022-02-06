using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;

namespace WPFLogViewer.Models
{
    internal abstract class INotifyPropertyChangedModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, args);
        }

        #endregion INotifyPropertyChanged

        #region PerfomanceHelpers

        protected void Raise<T>(Expression<Func<T>> propertyExpression)
        {
            AsyncRaise<T>(propertyExpression).SafeFireAndForget();
        }
        protected async Task AsyncRaise<T>(Expression<Func<T>> propertyExpression)
        {
            await Task.Yield();
            if (this.PropertyChanged != null)
            {
                MemberExpression body = propertyExpression.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("'propertyExpression' should be a member expression");

                object target = Expression.Lambda(body.Expression).Compile().DynamicInvoke();
                if (target is null)
                    return;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(body.Member.Name);
                try
                {
                    PropertyChanged(target, e);
                    OnPropertyRaised(target, e.PropertyName);
                }
                catch (Exception)
                {

                }
            }
        }

        protected void Raise<T>(params Expression<Func<T>>[] propertyExpressions)
        {
            foreach (Expression<Func<T>> propertyExpression in propertyExpressions)
            {
                Raise<T>(propertyExpression);
            }
        }

        protected virtual void OnPropertyRaised(object target, string PropertyName)
        {
        }

        #endregion PerfomanceHelpers

    }
}
