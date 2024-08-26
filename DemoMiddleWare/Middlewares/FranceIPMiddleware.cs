using System.Net;

namespace DemoMiddleWare.Middlewares
{
    public class FranceIPMiddleware
    {
        private readonly RequestDelegate _next;

        public FranceIPMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Je suis dans le middleware FranceIPMiddleware");

            string ipAddress = context.Connection.RemoteIpAddress?.ToString();

            // Vérifier si l'adresse IP est dans la plage d'adresses IP de la France
            bool isFromFrance = IsFrenchIP(ipAddress);

            if (!isFromFrance)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Access interdit. Il faut être en France pour accéder au site.");
                return;
            }

            // Passer la requête au prochain middleware dans le pipeline
            await _next(context);
        }

        private bool IsFrenchIP(string ipAddress)
        {
            // Exemple de vérification simple : vérifier si l'adresse IP commence par une plage d'adresses IP françaises
            // Vous devrez utiliser une méthode plus sophistiquée pour une vérification précise
            return ipAddress.StartsWith("80.") || ipAddress.StartsWith("90.");
        }
    }
}
