using ITKWebApi.Contexts;
using ITKWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITKWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoITKController : ControllerBase
    {
        private readonly ConexionSQLServer context;
        public ProductoITKController(ConexionSQLServer context)
        {
            this.context = context;
        }
        // GET: api/<ProductoITKController>
        [HttpGet]
        public IActionResult Get(string sku, string modelo)
        {
            try
            {
                List<ProductoITK> producto = new List<ProductoITK>();
                if(sku == null && modelo == null)
                {
                    //producto = context.Producto.ToList();
                    SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                    SqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.CommandText = "sp_FiltrarProductoTodos";
                    //comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value;
                    //comando.Parameters.Add("@sku", System.Data.SqlDbType.VarChar, 20).Value = sku;
                    //comando.Parameters.Add("@modelo", System.Data.SqlDbType.VarChar, 20).Value = modelo;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoITK productos = new ProductoITK();
                        productos.id = (int)reader["id"];
                        productos.SKU = (string)reader["sku"];
                        productos.Fert = (string)reader["fert"];
                        productos.Modelo = (string)reader["modelo"];
                        productos.Tipo = (string)reader["tipo"];
                        productos.NumeroSerie = (string)reader["numeroSerie"];
                        productos.Fecha = (DateTime)reader["fecha"];
                        producto.Add(productos);
                    }
                    conexion.Close();
                }
                else
                {
                    SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
                    SqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.CommandText = "sp_FiltrarProducto";
                    comando.Parameters.Add("@sku", System.Data.SqlDbType.VarChar, 20).Value = sku;
                    comando.Parameters.Add("@modelo", System.Data.SqlDbType.VarChar, 20).Value = modelo;
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductoITK productos = new ProductoITK();
                        productos.id = (int)reader["id"];
                        productos.SKU = (string)reader["sku"];
                        productos.Fert = (string)reader["fert"];
                        productos.Modelo = (string)reader["modelo"];
                        productos.Tipo = (string)reader["tipo"];
                        productos.NumeroSerie = (string)reader["numeroSerie"];
                        productos.Fecha = (DateTime)reader["fecha"];
                        producto.Add(productos);
                    }
                    conexion.Close();
                }
                return Ok(producto);
            }
            catch
            {
                return BadRequest("Error");
            }           
        }

        // GET api/<ProductoITKController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductoITKController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductoITKController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductoITK producto)
        {
            SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
            SqlCommand comando = conexion.CreateCommand();
            conexion.Open();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = "sp_editarProducto";
            comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = producto.id;
            comando.Parameters.Add("@sku", System.Data.SqlDbType.VarChar, 20).Value = producto.SKU;
            comando.Parameters.Add("@fert", System.Data.SqlDbType.VarChar, 20).Value = producto.Fert;
            comando.Parameters.Add("@modelo", System.Data.SqlDbType.VarChar, 20).Value = producto.Modelo;
            comando.Parameters.Add("@tipo", System.Data.SqlDbType.VarChar, 20).Value = producto.Tipo;
            comando.Parameters.Add("@numeroSerie", System.Data.SqlDbType.VarChar, 20).Value = producto.NumeroSerie;
            comando.Parameters.Add("@fecha", System.Data.SqlDbType.Date).Value = producto.Fecha;
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        // DELETE api/<ProductoITKController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            SqlConnection conexion = (SqlConnection)context.Database.GetDbConnection();
            SqlCommand comando = conexion.CreateCommand();
            conexion.Open();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = "sp_eliminarProducto";
            comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;            
            comando.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
