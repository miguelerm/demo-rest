using RestSinAuth.Models;
using RestSinAuth.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestSinAuth.Controllers
{
    public class UsuariosController : ApiController
    {
        private readonly UsuarioStorage usuarios;

        public UsuariosController()
        {
            usuarios = new UsuarioStorage();
        }

        public IHttpActionResult Get()
        {
            return Ok(usuarios.ObtenerTodos());
        }

        public IHttpActionResult Get(int id)
        {
            var usuario = usuarios.ObtenerUnico(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        public IHttpActionResult Post(CrearEditarUsuarioModel model)
        {
            var id = usuarios.Crear(model);

            if (id > 0)
            {
                var usuario = usuarios.ObtenerUnico(id);
                return CreatedAtRoute("default", new { controller = "usuarios", id = id }, usuario);
            }

            return InternalServerError();
        }

        public IHttpActionResult Put(int id, CrearEditarUsuarioModel model)
        {
            var modificado = usuarios.Editar(id, model);

            if (modificado)
            {
                return Ok();
            }

            return InternalServerError();
        }

        public IHttpActionResult Delete(int id)
        {
            var eliminado = usuarios.Eliminar(id);

            if (eliminado)
            {
                return Ok();
            }

            return InternalServerError();
        }

        [HttpPut]
        [Route("~/usuarios/{id}/activar")]
        public IHttpActionResult PutActivar(int id)
        {
            var activado = usuarios.Activar(id);

            if (activado)
            {
                return Ok();
            }

            return InternalServerError();
        }

        [HttpPut]
        [Route("~/usuarios/{id}/inactivar")]
        public IHttpActionResult PutInactivar(int id)
        {
            var activado = usuarios.Inactivar(id);

            if (activado)
            {
                return Ok();
            }

            return InternalServerError();
        }
    }
}
