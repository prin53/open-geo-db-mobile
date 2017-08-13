using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Exceptions;
using MvvmCross.Platform.Platform;
using OpenGeoDB.Services.Alert;

namespace OpenGeoDB.ViewModels
{
    public abstract class ViewModelBase<TParameter> : ViewModelBase, IMvxViewModel<TParameter>
        where TParameter : class
    {
        protected ViewModelBase(
            IMvxNavigationService navigationService,
            IAlertService alertService
        ) : base(navigationService, alertService)
        {
            /* Required constructor */
        }

        public async Task Init(string parameter)
        {
            if (!string.IsNullOrEmpty(parameter))
            {
                if (!Mvx.TryResolve(out IMvxJsonConverter serializer))
                {
                    throw new MvxIoCResolveException("There is no implementation of IMvxJsonConverter registered. You need to use the MvvmCross Json plugin or create your own implementation of IMvxJsonConverter.");
                }

                var deserialized = serializer.DeserializeObject<TParameter>(parameter);

                await Initialize(deserialized);
            }
        }

        public abstract Task Initialize(TParameter parameter);
    }
}
