using Zenject;

namespace MyPetProject.Infrastructure
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DimensionManager>().AsSingle();
        }
    }
}

