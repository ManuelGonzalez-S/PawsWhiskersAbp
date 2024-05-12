$(function () {
    var l = abp.localization.getResource('Cesta');
    var createModal = new abp.ModalManager(abp.appPath + 'Productos/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Productos/EditModal');

    var dataTable = $('#ProductosTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(cesta.productos.producto.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('Cesta.Productos.Edit'), //CHECK for the PERMISSION
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('Cesta.Productos.Delete'), //CHECK for the PERMISSION
                                    confirmMessage: function (data) {
                                        return l('ProductoDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        cesta.productos.producto
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('ProductoType'),
                    data: "productoType",
                    render: function (data) {
                        return l('Enum:ProductoType.' + data);
                    }
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {

                        var fecha = moment(data);

                        return fecha.format("DD/MM/YYYY");
                    }
                },
                {
                    title: l('Price'),
                    data: "price"
                },
                {
                    title: l('MascotaType'),
                    data: "mascotaType",
                    render: function (data) {
                        return l('Enum:MascotaType.' + data);
                    }

                }
            ]
        })
    );




    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProductoButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });


});