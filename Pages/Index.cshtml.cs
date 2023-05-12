using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace SessionForm.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        private const string SessionKey = "MeuFormulario";

        public class FormularioModel
        {
            public string DadoEtapa1 { get; set; }
            public int DadoEtapa2 { get; set; }
            public bool DadoEtapa3 { get; set; }
            public DateTime DadoEtapa4 { get; set; }
        }

        public async Task<IActionResult> OnPostEtapa1Async(FormularioModel formulario)
        {
            // Recuperar o objeto de dados da sessão
            var dadosFormularioBytes = await _httpContextAccessor.HttpContext.Session.GetByteArrayAsync(SessionKey);
            var dadosFormulario = dadosFormularioBytes == null ? new FormularioModel() : JsonSerializer.Deserialize<FormularioModel>(Encoding.UTF8.GetString(dadosFormularioBytes));

            // Atualizar os valores relevantes
            dadosFormulario.DadoEtapa1 = formulario.DadoEtapa1;

            // Armazenar o objeto atualizado na sessão novamente
            await _httpContextAccessor.HttpContext.Session.SetString(SessionKey, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dadosFormulario)));

            return RedirectToPage("./PaginaDoFormulario", "Etapa2");
        }

        public async Task<IActionResult> OnPostEtapa2Async(FormularioModel formulario)
        {
            // Recuperar o objeto de dados da sessão
            var dadosFormularioBytes = await _httpContextAccessor.HttpContext.Session.GetByteArrayAsync(SessionKey);
            var dadosFormulario = dadosFormularioBytes == null ? new FormularioModel() : JsonSerializer.Deserialize<FormularioModel>(Encoding.UTF8.GetString(dadosFormularioBytes));

            // Atualizar os valores relevantes
            dadosFormulario.DadoEtapa2 = formulario.DadoEtapa2;

            // Armazenar o objeto atualizado na sessão novamente
            await _httpContextAccessor.HttpContext.Session.SetString(SessionKey, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dadosFormulario)));

            return RedirectToPage("./PaginaDoFormulario", "Etapa3");
        }

        public async Task<IActionResult> OnPostEtapa3Async(FormularioModel formulario)
        {
            // Recuperar o objeto de dados da sessão
            var dadosFormularioBytes = await _httpContextAccessor.HttpContext.Session.GetByteArrayAsync(SessionKey);
            var dadosFormulario = dadosFormularioBytes == null ? new FormularioModel() : JsonSerializer.Deserialize<FormularioModel>(Encoding.UTF8.GetString(dadosFormularioBytes));

            // Atualizar os valores relevantes
            dadosFormulario.DadoEtapa3 = formulario.DadoEtapa3;

            // Armazenar o objeto atualizado na sessão novamente
            await _httpContextAccessor.HttpContext.Session.SetString(SessionKey, dadosFormulario);

            return RedirectToPage("./PaginaDoFormulario", "Etapa4");
        }

        public async Task<IActionResult> OnPostEtapa4(FormularioModel formulario)
        {
            // Recuperar o objeto de dados da sessão
            //var dadosFormulario = _httpContextAccessor.HttpContext.Session.GetAsync<FormularioModel>(SessionKey).Result ?? new FormularioModel();

            var dadosFormularioBytes = await _httpContextAccessor.HttpContext.Session.GetByteArrayAsync(SessionKey);
            var dadosFormulario = dadosFormularioBytes == null ? new FormularioModel() : JsonSerializer.Deserialize<FormularioModel>(Encoding.UTF8.GetString(dadosFormularioBytes));
            // Atualizar os valores relevantes
            dadosFormulario.DadoEtapa4 = formulario.DadoEtapa4;

            // Armazenar o objeto atualizado na sessão novamente
            await _httpContextAccessor.HttpContext.Session.SetString(SessionKey, dadosFormulario);

            // Finalizar o processo do formulário e limpar a sessão
            _httpContextAccessor.HttpContext.Session.Remove(SessionKey);

            return RedirectToPage("./PaginaDeSucesso");
        }
    }
}
