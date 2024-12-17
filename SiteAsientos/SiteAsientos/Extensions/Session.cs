using Newtonsoft.Json;

namespace SiteAsientos.Extensions
{
    public static class Session
    {
        //Guarda en sesion en formato json un usuario
        public static void GuardarObjeto(this ISession sesion, string key, object value)
        {
            string data = JsonConvert.SerializeObject(value);
            sesion.SetString(key, data);
        }

        //Devuelve el usuario en session
        public static T ObtenerUsuario<T>(this ISession sesion, string key)
        {
            string data = sesion.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}
