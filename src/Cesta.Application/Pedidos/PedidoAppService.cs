﻿using AutoMapper;
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
    public class PedidoAppService : IPedidoAppService

    {

        private readonly IRepository<Pedido, Guid> _pedidoAppRepository;

        private readonly IProductoAppService _productoAppService;

        private readonly ICurrentUser _currentUser;

        public IMapper _mapper;

        public PedidoAppService(IMapper mapper, IRepository<Pedido, Guid> pedidoAppRepository, ICurrentUser currentUser, IProductoAppService productoAppService)
        {
            //GetPolicyName = CestaPermissions.Productos.Default;
            //GetListPolicyName = CestaPermissions.Productos.Default;
            //CreatePolicyName = CestaPermissions.Productos.Create;
            //UpdatePolicyName = CestaPermissions.Productos.Edit;
            //DeletePolicyName = CestaPermissions.Productos.Delete;

            _mapper = mapper;

            _currentUser = currentUser;

            _productoAppService = productoAppService;

            _pedidoAppRepository = pedidoAppRepository;

        }

        #region Get

        public async Task<PedidoDto> GetAsync(Guid id)
        {
            byte[] bytes = id.ToByteArray();
            int convertedInt = BitConverter.ToInt32(bytes, 0);

            var pedido = await GetByPedidoIdAsync(convertedInt);
            return _mapper.Map<Pedido, PedidoDto>(pedido);
        }

        public async Task<PedidoDto> GetById(int id)
        {
            var pedido = GetByPedidoIdAsync(id);
            return _mapper.Map<PedidoDto>(pedido);
        }

        public async Task<List<PedidoDto>> GetByUserId(Guid userId)
        {

            Check.NotNull(userId, nameof(userId));

            var pedidosUser = await GetListByUserId(userId);

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

        public async Task<List<PedidoDto>> GetListByUserId(Guid idUser)
        {
            Check.NotNull(idUser, nameof(idUser));

            // Obtener la lista de pedidos y filtrar por el idGuid
            var resultado = await _pedidoAppRepository.GetListAsync();

            var pedidos = resultado.Where(x => x.UsuarioId == idUser).ToList();

            if (!pedidos.Any())
            {
                //Caso en que no se encuentren pedidos
                throw new PedidoNotFoundException(nameof(Pedido));
            }

            var pedidosDtos = _mapper.Map<List<Pedido>, List<PedidoDto>>(pedidos);
            return pedidosDtos;
        }

        public Task<PagedResultDto<PedidoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Create
        public async Task<PedidoDto> CreateAsync(CreateUpdatePedidoDto dto)
        {

            var idProducto = await _productoAppService.GetByIdAsync(dto.ProductoId);

            // Obtener la lista de productos desde _productoAppService
            var listaProductosBBDD = await _productoAppService.ListAsync();

            // Recoger la lista de pedidos del usuario en el carrito
            var listPedidosUser = await GetListByUserId((Guid)_currentUser.Id);

            // Recoger los productos de la lista de pedidos
            List<Producto> listProductosCestaUser = new List<Producto>();

            foreach (var item in listPedidosUser)
            {
                var productoDto = listaProductosBBDD.FirstOrDefault(p => p.Id == item.ProductoId);
                if (productoDto != null)
                {
                    var producto = _mapper.Map<Producto>(productoDto);
                    listProductosCestaUser.Add(producto);
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
            pedido.Cantidad = 0;
            pedido.ProductoId = idProducto.Id;

            // Insertar el pedido en el repositorio
            await _pedidoAppRepository.InsertAsync(pedido);

            // Mapear el objeto pedido a PedidoDto y devolverlo
            var pedidoDto = _mapper.Map<PedidoDto>(pedido);

            return pedidoDto;
        }


        public async Task<PedidoDto> CreateAsync(int dto)
        {

            Guid guidGenerado = new Guid(dto, 0, 0, new byte[8]);

            var idProducto = await _productoAppService.GetByIdAsync(guidGenerado);

            // Obtener la lista de productos desde _productoAppService
            var listaProductosBBDD = await _productoAppService.ListAsync();

            // Recoger la lista de pedidos del usuario en el carrito
            var listPedidosUser = await GetListByUserId((Guid)_currentUser.Id);

            // Recoger los productos de la lista de pedidos
            List<Producto> listProductosCestaUser = new List<Producto>();

            foreach (var item in listPedidosUser)
            {
                var productoDto = listaProductosBBDD.FirstOrDefault(p => p.Id == item.ProductoId);
                if (productoDto != null)
                {
                    var producto = _mapper.Map<Producto>(productoDto);
                    listProductosCestaUser.Add(producto);
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
            pedido.Cantidad = 0;
            pedido.ProductoId = idProducto.Id;

            // Insertar el pedido en el repositorio
            await _pedidoAppRepository.InsertAsync(pedido);

            // Mapear el objeto pedido a PedidoDto y devolverlo
            var pedidoDto = _mapper.Map<PedidoDto>(pedido);

            return pedidoDto;
        }
        #endregion

        #region Update
        public async Task UpdateAsync(Guid id, PedidoDto input)
        {

            byte[] guidBytes = id.ToByteArray();
            int resultado;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(guidBytes);
                resultado = BitConverter.ToInt32(hashBytes, 0);
            }

            var pedido = await GetByPedidoIdAsync(resultado);

            if((pedido != null) && (pedido.ProductoId == input.ProductoId) && (pedido.UsuarioId == input.UsuarioId))
            {
                pedido.Cantidad = input.Cantidad;

                await _pedidoAppRepository.UpdateAsync(pedido);
            }
            else
            {
                throw new Exception("datosErroneos");
            }

        }

        public async Task<PedidoDto> UpdateAsync(Guid id, CreateUpdatePedidoDto input)
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
        public async Task DeleteAsync(Guid id)
        {
            await _pedidoAppRepository.DeleteAsync(id);
        }
        #endregion

    }
}