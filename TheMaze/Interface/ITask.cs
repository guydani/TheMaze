namespace Server.Interface
{
    public interface ITask
    {
        int NumberOfTask { get; set; }
        void SetCommand(string[] s, int index);
        void HandleTask();
    }
}