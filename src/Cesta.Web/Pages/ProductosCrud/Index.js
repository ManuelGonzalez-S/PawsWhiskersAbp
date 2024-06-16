var createModal = new abp.ModalManager(abp.appPath + 'ProductosCrud/CreateModal');
var editModal = new abp.ModalManager(abp.appPath + 'ProductosCrud/EditModal');
var l = abp.localization.getResource('Cesta');
var datatable;
var Toast;
$(document).ready(async function () {

    Toast = Swal.mixin({
        toast: true,
        position: 'center',
        iconColor: 'white',
        customClass: {
            popup: 'colored-toast',
        },
        showConfirmButton: false,
        timer: 1500,
        timerProgressBar: true,
    })

    $('#loadingGif').show();
    $('#NewProductoButton').hide()

    await cargarTabla();
    await new Promise(resolve => setTimeout(resolve,2000))
    $('#loadingGif').hide();
    $('#NewProductoButton').show();
    


    createModal.onResult(function (event, jqXHR) {
        // Asegúrate de estar obteniendo el JSON de la respuesta
        const result = jqXHR.responseText;
        console.log(result);

        if (result.success) {
            Toast.fire({
                icon: 'success',
                title: l('ProductoCreatedSuccesfully'),
            })

            //abp.notify.info(l('ProductoCreatedSuccesfully'));
            dataTable.ajax.reload();
        } else {
            if (result.errors) {
                // Mostrar todos los errores de validación
                result.errors.forEach(function (error) {
                    abp.notify.error(error);
                });
            } else {
                Toast.fire({
                    icon: 'error',
                    title: l('UnexpectedError'),
                })
            }
        }
    });

    editModal.onResult(function (event, jqXHR) {
        try {
            const result = jqXHR.responseText;
            console.log(result);

            if (result.success) {
                Toast.fire({
                    icon: 'success',
                    title: l('ProductoEditedSuccesfully'),
                })
                dataTable.ajax.reload();
            } else {
                if (result.errors) {
                    // Mostrar todos los errores de validación
                    result.errors.forEach(function (error) {
                        abp.notify.error(error);
                    });
                } else {
                    Toast.fire({
                        icon: 'error',
                        title: l('UnexpectedError'),
                    })
                }
            }
        } catch (e) {
            console.error('Error parsing JSON response', e);
            abp.notify.error(l('UnexpectedError'));
        }
    });


    $('#NewProductoButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });


});
function cargarTabla() {
    dataTable = $('#ProductosTable').DataTable(
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
                    title: l('MascotaType'),
                    data: "mascotaType",
                    render: function (data) {
                        return l('Enum:MascotaType.' + data);
                    }
                },
                {
                    title: l('Price'),
                    data: "price",
                    render: function (data) {
                        return data + '€';
                    }
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {

                        var fecha = moment(data);

                        return fecha.format("DD/MM/YYYY");
                    }
                }
            ]
        })
    );

};

    ; (async () => {
        Toast.fire({
            icon: 'success',
            title: 'Success',
        })
        Toast.fire({
            icon: 'error',
            title: 'Error',
        })
        Toast.fire({
            icon: 'warning',
            title: 'Warning',
        })
        Toast.fire({
            icon: 'info',
            title: 'Info',
        })
        Toast.fire({
            icon: 'question',
            title: 'Question',
        })
    })()