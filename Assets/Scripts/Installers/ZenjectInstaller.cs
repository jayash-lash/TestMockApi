using Common;
using RestAPI.Service;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ZenjectInstaller : MonoInstaller
    {
        [SerializeField] private InfoButtonFactory _buttonFactory;
        [SerializeField] private MainButtonView _mainButtonView;
        [SerializeField] private PopViewUI _popViewUI;
        [SerializeField] private ErrorPopUp _errorPopUp;
        public override void InstallBindings()
        {
            Container.Bind<IHttpClient>().To<MockIOButtonsHttpClient>().AsSingle();
            Container.Bind<ISerializer>().To<JsonNetSerializer>().AsSingle();
            Container.Bind<ITransport>().To<UnityWebRequestTransport>().AsSingle();
            Container.Bind<IButtonsRepository>().To<ButtonsRepository>().AsSingle();
            
            Container.Bind<InfoButtonFactory>().FromInstance(_buttonFactory);
            Container.Bind<MainButtonView>().FromInstance(_mainButtonView);
            Container.Bind<PopViewUI>().FromInstance(_popViewUI);
            Container.Bind<ErrorPopUp>().FromInstance(_errorPopUp);
        }
    }
}