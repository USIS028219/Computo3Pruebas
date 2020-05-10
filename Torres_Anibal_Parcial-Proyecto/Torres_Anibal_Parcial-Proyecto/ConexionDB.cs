using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Torres_Anibal_Parcial
{
    class ConexionDB
    {
        SqlConnection miConexion = new SqlConnection();
        SqlCommand comandosSQL = new SqlCommand();
        SqlDataAdapter miAdaptadorDatos = new SqlDataAdapter();

        DataSet ds = new DataSet();

        public ConexionDB()
        {
            String cadena = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SistemaDB.mdf;Integrated Security=True";
            miConexion.ConnectionString = cadena;
            miConexion.Open();

            parametrizacion();
        }
        private void parametrizacion()
        {
            comandosSQL.Parameters.Add("@id", SqlDbType.Int).Value = 0;
            comandosSQL.Parameters.Add("@idC", SqlDbType.Int).Value = 0;
            comandosSQL.Parameters.Add("@cod", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@nom", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@ap", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@dir", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@dui", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@tel", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@sa", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@car", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@em", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@nomPro", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@nomCont", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@des", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@pre", SqlDbType.Char).Value = "";
            comandosSQL.Parameters.Add("@nomCli", SqlDbType.Char).Value = "";
        }

        public DataSet Obtener_datos()
        {
            ds.Clear();
            comandosSQL.Connection = miConexion;

            comandosSQL.CommandText = "select * from usuarios";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "usuarios");

            comandosSQL.CommandText = "select * from empleados";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "empleados");

            comandosSQL.CommandText = "select * from proveedores";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "proveedores");

            comandosSQL.CommandText = "select * from productos";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "productos");

            comandosSQL.CommandText = "select * from categorias";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "categorias");

            comandosSQL.CommandText = "select * from pagos";
            miAdaptadorDatos.SelectCommand = comandosSQL;
            miAdaptadorDatos.Fill(ds, "pagos");

            return ds;
        }
        public void Mantenimiento_usuarios(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "Nuevo")
            {
                sql = "INSERT INTO usuarios (codigo,nombre,apellido,direccion,dui,telefono) VALUES( @cod, @nom, @ap, @dir, @dui, @tel)";
            }
            else if (accion == "Modificar")
            {
                sql = "UPDATE usuarios SET " +
                    "codigo          = @cod," +
                    "nombre          = @nom," +
                    "apellido        = @ap," +
                    "direccion       = @dir," +
                    "dui             = @dui," +
                    "telefono        = @tel " +
                    "WHERE idusuario = @id";
            }
            else if (accion == "Eliminar")
            {
                sql = "DELETE usuarios FROM usuarios WHERE idusuario= @id";
            }
            comandosSQL.Parameters["@id"].Value = datos[0];
            if (accion != "eliminar")
            {
                comandosSQL.Parameters["@cod"].Value = datos[1];
                comandosSQL.Parameters["@nom"].Value = datos[2];
                comandosSQL.Parameters["@ap"].Value = datos[3];
                comandosSQL.Parameters["@dir"].Value = datos[4];
                comandosSQL.Parameters["@dui"].Value = datos[5];
                comandosSQL.Parameters["@tel"].Value = datos[6];
            }

            ProcesarSQL(sql);
        }
        public void Mantenimiento_empleados(String[] datos, String accion)
        {
            String sql = "";
            if (accion == "Nuevo")
            {
                sql = "INSERT INTO empleados (codigo,nombre,apellido,direccion,dui,telefono,salario) VALUES( @cod, @nom, @ap, @dir, @dui, @tel, @sa) ";
            }
            else if (accion == "Modificar")
            {
                sql = "UPDATE empleados SET " +
                    "codigo          = @cod," +
                    "nombre          = @nom," +
                    "apellido        = @ap," +
                    "direccion       = @dir," +
                    "dui             = @dui," +
                    "telefono        = @tel," +
                    "salario         = @sa " +
                    "WHERE idempleado= @id";
            }
            else if (accion == "Eliminar")
            {
                sql = "DELETE empleados FROM empleados WHERE idempleado= @id";
            }
            comandosSQL.Parameters["@id"].Value = datos[0];
            if (accion != "eliminar")
            {
                comandosSQL.Parameters["@cod"].Value = datos[1];
                comandosSQL.Parameters["@nom"].Value = datos[2];
                comandosSQL.Parameters["@ap"].Value = datos[3];
                comandosSQL.Parameters["@dir"].Value = datos[4];
                comandosSQL.Parameters["@dui"].Value = datos[5];
                comandosSQL.Parameters["@tel"].Value = datos[6];
                comandosSQL.Parameters["@sa"].Value = datos[7];
            }

            ProcesarSQL(sql);
        }
        public void Mantenimiento_proveedores(string[] datos, string accion)
        {
            string sql = "";
            if (accion == "Nuevo")
            {
                sql = "insert into proveedores(codigo,nombrep,nombrec,cargoc,direccion,telefono,email) values( @cod, @nomPro, @nomCont, @car, @dir, @tel, @em) ";
            }
            else if (accion == "Modificar")
            {
                sql = "update proveedores set  " +
                   "codigo         = @cod, " +
                    "nombrep        = @nomPro, " +
                    "nombrec        = @nomCont, " +
                    "cargoc         = @car, " +
                    "direccion      = @dir, " +
                    "telefono       = @tel, " +
                    "email          = @em  " +
                    "where idproveedor = @id";
            }
            else if (accion == "Eliminar")
            {
                sql = "delete proveedores from proveedores where idproveedor = @id";
            }
            comandosSQL.Parameters["@id"].Value = datos[0];
            if (accion != "eliminar")
            {
                comandosSQL.Parameters["@cod"].Value = datos[1];
                comandosSQL.Parameters["@nomPro"].Value = datos[2];
                comandosSQL.Parameters["@nomCont"].Value = datos[3];
                comandosSQL.Parameters["@car"].Value = datos[4];
                comandosSQL.Parameters["@dir"].Value = datos[5];
                comandosSQL.Parameters["@tel"].Value = datos[6];
                comandosSQL.Parameters["@em"].Value = datos[7];
            }
            
            ProcesarSQL(sql);
        }
        public void Mantenimiento_productos_categorias(string[] datos, string accion)
        {
            string sql = "";
            if (accion=="Nuevo")
            {
                sql = "insert into productos(idcategoria,codigo,nombre,descripcion,precio) values( @idC, @cod, @nom, @des, @pre) ";
            }
            else if (accion=="Modificar")
            {
                sql = "update productos set  " +
                    "idcategoria            = @idC," +
                   "codigo                 = @cod," +
                   "nombre                 = @nom," +
                   "descripcion            = @des," +
                   "precio                 = @pre " +
                   "where idproducto =  @id";
            }
            else
            {
                sql = "delete productos from productos where idproducto= @id";
            }
            comandosSQL.Parameters["@id"].Value = datos[0];
            if (accion != "eliminar")
            {
                comandosSQL.Parameters["@idC"].Value = datos[1];
                comandosSQL.Parameters["@cod"].Value = datos[2];
                comandosSQL.Parameters["@nom"].Value = datos[3];
                comandosSQL.Parameters["@des"].Value = datos[4];
                comandosSQL.Parameters["@pre"].Value = datos[5];
            }

            ProcesarSQL(sql);
        }
        public void Mantenimiento_pagos(string[] datos, string accion)
        {
            string sql = "";
            if (accion == "Nuevo")
            {
                sql = "insert into pagos(idempleado,cargo,tipopago,fechap) values( @cod, @nom, @des, @pre, @nomCli, @tel ) ";
            }
            else if (accion == "Modificar")
            {
                sql = "update pagos set  " +
                   "codigo         = @cod," +
                    "nombre         = @nom," +
                    "descripcion    = @des," +
                    "precio         = @pre," +
                    "nombrec        = @nomCli," +
                    "telefono       = @tel " +
                    "WHERE IdVentas = @id ";
            }
            else
            {
                sql = "delete pagos from pagos where idpago= @id";
            }
            comandosSQL.Parameters["@id"].Value = datos[0];
            if (accion != "eliminar")
            {
                comandosSQL.Parameters["@cod"].Value = datos[1];
                comandosSQL.Parameters["@nom"].Value = datos[2];
                comandosSQL.Parameters["@des"].Value = datos[3];
                comandosSQL.Parameters["@pre"].Value = datos[4];
                comandosSQL.Parameters["@nomCli"].Value = datos[5];
                comandosSQL.Parameters["@tel"].Value = datos[6];
            }

            ProcesarSQL(sql);
        }
        void ProcesarSQL(String sql)
        {
            comandosSQL.Connection = miConexion;
            comandosSQL.CommandText = sql;
            comandosSQL.ExecuteNonQuery();
        }
    }
}       