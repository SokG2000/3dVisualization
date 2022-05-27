namespace Assets.Scripts.Runtime
{
    public interface IController
    {
        void OnStart(); // создание
        void OnStop(); // остановка
        void Tick(); // каждый такт
    }
}