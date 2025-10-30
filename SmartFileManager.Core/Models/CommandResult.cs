namespace SmartFileManager.Core.Models
{
    public enum CommandStatus
    {
        Success,
        Error,
        Exit,
    }

    //универсальный результат выполнения команды (успех/ошибка, сообщение, данные). Мост между Core и App.
    public record class CommandResult
    {
        public CommandStatus Status { get; set; } //команда выполнилась или нет.
        public string Message { get; set; } = string.Empty; //текстовое сообщение для App (UI).
        // при необходимости можно добавить данные, например, список файлов
        public object? Data { get; set; } //если команда возвращает что-то, например, список файлов (List<FileItem>).
    }
}
