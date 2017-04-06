using Loja.DAO;
using Loja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LojaAPI.Controllers
{
    public class CarrinhoController : ApiController
    {
        public HttpResponseMessage Get(long id)
        {
            try
            {
                CarrinhoDAO dao = new CarrinhoDAO();
                var carrinho = dao.Busca(id);
                return Request.CreateResponse(HttpStatusCode.OK, carrinho);
            }
            catch (Exception)
            {
                var msg = string.Format("O carrinho {0} não foi encontrado", id);
                var httperror = new HttpError(msg);
                return Request.CreateResponse(HttpStatusCode.NotFound, httperror);
            }            
        }
        
        public HttpResponseMessage Post([FromBody] Carrinho carrinho)
        {
            CarrinhoDAO dao = new CarrinhoDAO();
            dao.Adiciona(carrinho);
            HttpResponseMessage resp = Request.CreateResponse(HttpStatusCode.Created);
            string location = Url.Link("DefaultApi", new { controller = "carrinho", id = carrinho.Id });
            resp.Headers.Location = new Uri(location);
            return resp;
        }

    }
}
