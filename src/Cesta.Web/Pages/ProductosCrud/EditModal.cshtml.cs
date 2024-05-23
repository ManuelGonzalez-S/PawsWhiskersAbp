using System;
using System.Threading.Tasks;
using Cesta.Productos;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Cesta.Web.Pages.ProductosCrud;

public class EditModalModel : CestaPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateUpdateProductoDto Producto { get; set; }

    private readonly IProductoAppService _productoAppService;

    public EditModalModel(IProductoAppService productoAppService)
    {
        _productoAppService = productoAppService;
    }

    public async Task OnGetAsync()
    {
        var productoDto = await _productoAppService.GetAsync(Id);
        Producto = ObjectMapper.Map<ProductoDto, CreateUpdateProductoDto>(productoDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _productoAppService.UpdateAsync(Id, Producto);
        return NoContent();
    }
}