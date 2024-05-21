using Zenject;


namespace MyPetProject
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DimensionManager>().AsSingle();
        }
    }
}

