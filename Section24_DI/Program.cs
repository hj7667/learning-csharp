using Microsoft.Extensions.DependencyInjection;
using Section24_DI.ManualInjection;

var sender = new EmailSender();
var service = new NotificationService(sender);
service.Notify("안녕하세요");