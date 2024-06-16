using AutoMapper;
using AutoMapper.Internal.Mappers;
using Cesta.Permissions;
using Cesta.Productos;
using Scriban.Runtime.Accessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;

namespace Cesta.Pedidos
{
    public class PedidoAppService : CrudAppService<
            Pedido, //The Producto entity
            PedidoDto, //Used to show productos
            Guid, //Primary key of the producto entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdatePedidoDto>, //Used to create/update a producto
        IPedidoAppService

    {

        private readonly IRepository<Pedido, Guid> _pedidoAppRepository;

        private readonly IProductoAppService _productoAppService;

        private readonly ICurrentUser _currentUser;

        public IMapper _mapper;

        public PedidoAppService(IRepository<Pedido, Guid> repository, IMapper mapper, ICurrentUser currentUser, IProductoAppService productoAppService) : base(repository)
        {
            //GetPolicyName = CestaPermissions.Productos.Default;
            //GetListPolicyName = CestaPermissions.Productos.Default;
            //CreatePolicyName = CestaPermissions.Productos.Create;
            //UpdatePolicyName = CestaPermissions.Productos.Edit;
            //DeletePolicyName = CestaPermissions.Productos.Delete;

            _mapper = mapper;

            _currentUser = currentUser;

            _productoAppService = productoAppService;

            _pedidoAppRepository = repository;

        }

        #region Get

        public override async Task<PedidoDto> GetAsync(Guid id)
        {
            byte[] bytes = id.ToByteArray();
            int convertedInt = BitConverter.ToInt32(bytes, 0);

            var pedido = await GetByPedidoIdAsync(convertedInt);
            return _mapper.Map<Pedido, PedidoDto>(pedido);
        }

        public async Task<PedidoDto> GetById(int id)
        {
            var pedido = await GetByPedidoIdAsync(id);
            return _mapper.Map<PedidoDto>(pedido);
        }

        public async Task<List<PedidoDto>> GetByUserId()
        {

            if(_currentUser.Id == null)
            {
                throw new NotImplementedException();
            }

            var userId = _currentUser.Id;

            Check.NotNull(userId, nameof(userId));

            var pedidosUser = await GetListByCurrentUser();

            return pedidosUser;

        }

        private async Task<Pedido> GetByPedidoIdAsync(int id)
        {
            Check.NotNull(id, nameof(id));

            // Convertir el int a Guid
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(id).CopyTo(bytes, 0);
            var idGuid = new Guid(bytes);

            // Obtener la lista de pedidos y filtrar por el idGuid
            var resultado = await _pedidoAppRepository.GetListAsync();
            var pedido = resultado.FirstOrDefault(x => x.Id == idGuid);

            if (pedido == null)
            {
                // Manejar el caso en que no se encuentre el pedido
                // Puede ser lanzar una excepción, retornar null, etc.
                throw new PedidoNotFoundException(nameof(Pedido));
            }

            return pedido;
        }

        public async Task<PedidoDto> GetPedidoDtoByProductoId(Guid idProducto)
        {
            if (_currentUser.Id == null)
            {
                throw new NotImplementedException();
            }

            var idUser = _currentUser.Id;

            // Obtener la lista de pedidos y filtrar por el idGuid
            var resultado = await _pedidoAppRepository.GetListAsync();

            var pedido = resultado.FirstOrDefault(x => x.UsuarioId == idUser && x.ProductoId == idProducto);

            var pedidoDto = _mapper.Map<Pedido, PedidoDto>(pedido);
            return pedidoDto;

        }

        public async Task<List<PedidoDto>> GetListByCurrentUser()
        {

            if(_currentUser.Id == null)
            {
                throw new NotImplementedException();
            }

            var idUser = _currentUser.Id;

            // Obtener la lista de pedidos y filtrar por el idGuid
            var resultado = await _pedidoAppRepository.GetListAsync();

            var pedidos = resultado.Where(x => x.UsuarioId == idUser).ToList();

            //if (!pedidos.Any())
            //{
            //    //Caso en que no se encuentren pedidos
            //    throw new PedidoNotFoundException(nameof(Pedido));
            //}

            var pedidosDtos = _mapper.Map<List<Pedido>, List<PedidoDto>>(pedidos);
            return pedidosDtos;
        }

        public override async Task<PagedResultDto<PedidoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Create
        public override async Task<PedidoDto> CreateAsync(CreateUpdatePedidoDto dto)
        {

            var idProducto = await _productoAppService.GetByIdAsync(dto.ProductoId);

            // Recoger la lista de pedidos del usuario en el carrito
            var listPedidosUser = await GetListByCurrentUser();

            // Recoger los productos de la lista de pedidos
            List<Producto> listProductosCestaUser = new List<Producto>();

            //RECOGER PRODUCTOS EN LA CESTA DEL USER
            if (idProducto != null)
            {
                foreach (var item in listPedidosUser)
                {

                    var producto = item.ProductoId;

                    var productoResponse = await _productoAppService.GetByIdAsync(producto);

                    //FILTRAR PARA COGER LOS PRODUCTOS QUE TIENE EL USUARIO
                    var productoEntity = _mapper.Map<Producto>(productoResponse);
                    listProductosCestaUser.Add(productoEntity);

                }
            }

            // Comprobar si el producto ya existe en la cesta del usuario
            if (listProductosCestaUser.Any(p => p.Id == idProducto.Id))
            {
                throw new Exception("El producto ya existe en la cesta del usuario.");
            }

            // Crear el pedido
            var pedido = new Pedido();

            pedido.UsuarioId = (Guid)_currentUser.Id;
            pedido.Cantidad = 1;
            pedido.ProductoId = idProducto.Id;

            // Insertar el pedido en el repositorio
            await _pedidoAppRepository.InsertAsync(pedido);

            // Mapear el objeto pedido a PedidoDto y devolverlo
            var pedidoDto = _mapper.Map<PedidoDto>(pedido);

            return pedidoDto;
        }


        public async Task<PedidoDto> CreateAsyncGuidProducto(Guid productoId)
        {

            var idProducto = await _productoAppService.GetByIdAsync(productoId);

            // Recoger la lista de pedidos del usuario en el carrito
            var listPedidosUser = await GetListByCurrentUser();

            // Recoger los productos de la lista de pedidos
            List<Producto> listProductosCestaUser = new List<Producto>();




            //RECOGER PRODUCTOS EN LA CESTA DEL USER

            if (idProducto != null)
            {
                foreach (var item in listPedidosUser)
                {

                    var producto = item.ProductoId;

                    var productoResponse = await _productoAppService.GetByIdAsync(producto);

                    //FILTRAR PARA COGER LOS PRODUCTOS QUE TIENE EL USUARIO
                    var productoEntity = _mapper.Map<Producto>(productoResponse);
                    listProductosCestaUser.Add(productoEntity);

                }
            }

            // Comprobar si el producto ya existe en la cesta del usuario
            if (listProductosCestaUser.Any(p => p.Id == idProducto.Id))
            {
                throw new Exception("El producto ya existe en la cesta del usuario.");
            }

            // Crear el pedido
            var pedido = new Pedido();

            pedido.UsuarioId = (Guid)_currentUser.Id;
            pedido.Cantidad = 1;
            pedido.ProductoId = idProducto.Id;

            // Insertar el pedido en el repositorio
            await _pedidoAppRepository.InsertAsync(pedido);

            // Mapear el objeto pedido a PedidoDto y devolverlo
            var pedidoDto = _mapper.Map<PedidoDto>(pedido);

            return pedidoDto;
        }
        #endregion

        #region Update
        public override async Task<PedidoDto> UpdateAsync(Guid id, CreateUpdatePedidoDto input)
        {
            byte[] guidBytes = id.ToByteArray();
            int resultado;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(guidBytes);
                resultado = BitConverter.ToInt32(hashBytes, 0);
            }

            var pedido = await GetByPedidoIdAsync(resultado);

            if ((pedido != null) && (pedido.ProductoId == input.ProductoId) && (pedido.UsuarioId == input.UsuarioId))
            {
                pedido.Cantidad = input.Cantidad;

                await _pedidoAppRepository.UpdateAsync(pedido);

                return _mapper.Map<Pedido, PedidoDto>(pedido);
            }
            else
            {
                throw new Exception("datosErroneos");
            }
        }

        public async Task<PedidoDto> UpdateAsync(Guid id, PedidoDto input)
        {
            byte[] guidBytes = id.ToByteArray();
            int resultado;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(guidBytes);
                resultado = BitConverter.ToInt32(hashBytes, 0);
            }

            var pedido = await GetByPedidoIdAsync(resultado);

            if ((pedido != null) && (pedido.ProductoId == input.ProductoId) && (pedido.UsuarioId == input.UsuarioId))
            {
                pedido.Cantidad = input.Cantidad;

                await _pedidoAppRepository.UpdateAsync(pedido);

                return _mapper.Map<Pedido, PedidoDto>(pedido);
            }
            else
            {
                throw new Exception("datosErroneos");
            }
        }
        #endregion

        #region Delete
        public override async Task DeleteAsync(Guid id)
        {
            await _pedidoAppRepository.DeleteAsync(id);
        }

        public async Task<ProductoDto> DeleteByProductoUserId(Guid idProducto)
        {

            // Asegurarse de que el usuario actual esté autenticado
            if (_currentUser.Id == null)
            {
                throw new NotImplementedException();
            }

            var userId = _currentUser.Id;

            // Obtener la lista de pedidos del usuario actual
            List<PedidoDto> listaPedidosUser = await GetListByCurrentUser();

            // Filtrar los pedidos que tienen el mismo productoId y usuarioId
            var pedidosAEliminar = listaPedidosUser
                .Where(x => x.UsuarioId == userId && x.ProductoId == idProducto)
                .ToList();

            // Eliminar el pedido filtrado (aunque solo sea uno)
            foreach (var pedidoDto in pedidosAEliminar)
            {
                await _pedidoAppRepository.DeleteAsync(pedidoDto.Id);
            }

            // Devolver el ProductoDto del producto eliminado, suponiendo que se quiere devolver alguna información del producto eliminado
            // Nota: Esto asume que existe un método para obtener un ProductoDto por idProducto
            var productoDto = await _productoAppService.GetByIdAsync(idProducto);

            return productoDto;

        }
        #endregion

    }
}
