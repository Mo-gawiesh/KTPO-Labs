using KTPO4311.Ziarmal.Lib.src.SampleCommands;

namespace KTPO4311.Ziarmal.Lib.src.SampleCommands
{
    public interface ISampleCommand
    {
        void Execute();
    }

    public class FirstCommand : ISampleCommand
    {
        private readonly IView view;
        private int iExecute = 0;
        public FirstCommand(IView view)
        {
            this.view = view; 
        }
        public void Execute()
        {
            iExecute++;
            view.Render(this.GetType().ToString() + "\n iExecute = " + iExecute);
        }
    }

    public class SecondCommand : ISampleCommand
    {
        private readonly IView view;
        private int iExecute = 0;
        public SecondCommand(IView view)
        {
            this.view = view;
        }
        public void Execute()
        {
            iExecute++;
            view.Render(this.GetType().ToString() + "\n iExecute = " + iExecute);
            throw new System.Exception("fake exception");
        }
    }

}
