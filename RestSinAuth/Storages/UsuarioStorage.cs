using Dapper;
using RestSinAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSinAuth.Storages
{
    public class UsuarioStorage
    {
        private Conexion connection;

        public UsuarioStorage()
        {
            connection = new Conexion();
        }

        public IEnumerable<UsuarioModel> ObtenerTodos()
        {
            using (var conn = connection.Open())
            {
                return conn.Query<UsuarioModel>("SELECT * FROM USUARIOS ORDER BY ID").ToArray();
            }
        }

        public UsuarioModel ObtenerUnico(int id)
        {
            using (var conn = connection.Open())
            {
                return conn.Query<UsuarioModel>("SELECT * FROM USUARIOS WHERE ID = @ID", new { id }).FirstOrDefault();
            }
        }

        public int Crear(CrearEditarUsuarioModel usuario)
        {
            using (var conn = connection.Open())
            {
                var filasInsertadas = conn.Execute("INSERT INTO USUARIOS (CORREO, NOMBRES, APELLIDOS, FECHANACIMIENTO) VALUES (@CORREO, @NOMBRES, @APELLIDOS, @FECHANACIMIENTO)", usuario);
                if (filasInsertadas == 1)
                {
                    return (int)conn.ExecuteScalar<long>("SELECT last_insert_rowid()");
                }
            }

            return 0;
        }

        public bool Editar(int id, CrearEditarUsuarioModel usuario)
        {
            using (var conn = connection.Open())
            {
                var filasAlteradas = conn.Execute("UPDATE USUARIOS SET CORREO = @CORREO, NOMBRES = @NOMBRES, APELLIDOS = @APELLIDOS, FECHANACIMIENTO = @FECHANACIMIENTO WHERE ID = @ID", new { id, usuario.Correo, usuario.Nombres, usuario.Apellidos, usuario.FechaNacimiento });
                return filasAlteradas == 1;
            }
        }

        public bool Eliminar(int id)
        {
            using (var conn = connection.Open())
            {
                var filasEliminadas = conn.Execute("DELETE FROM USUARIOS WHERE ID = @ID", new { id });
                return filasEliminadas == 1;
            }
        }

        public bool Activar(int id)
        {
            return CambiarEstado(id, true);
        }

        public bool Inactivar(int id)
        {
            return CambiarEstado(id, false);
        }

        private bool CambiarEstado(int id, bool activo)
        {
            using (var conn = connection.Open())
            {
                var filasAlteradas = conn.Execute("UPDATE USUARIOS SET ACTIVO = @ACTIVO WHERE ID = @ID", new { id, activo });
                return filasAlteradas == 1;
            }
        }
    }
}
