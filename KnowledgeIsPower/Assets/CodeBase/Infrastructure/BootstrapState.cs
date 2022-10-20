using System.Net.Mime;
using CodeBase.Services.Input;
using UnityEditor.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly Game.SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, Game.SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            //RegisterServices();
            
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            
        }

        public void Exit()
        {
            
        }

        /*private void RegisterServices()
        {
            Game.InputService = SceneSetup;
        }
        
        private static void RegisterInputInput()
        {
            if (MediaTypeNames.Application.isEditor)
                InputService = new StandaloneInputService();
            else
                InputService = new MobileInputService();
        }*/
    }
}