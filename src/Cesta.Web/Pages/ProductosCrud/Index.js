$(function () {
    var l = abp.localization.getResource('Cesta');
    var createModal = new abp.ModalManager(abp.appPath + 'ProductosCrud/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'ProductosCrud/EditModal');

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


    createModal.onResult(function (event, jqXHR) {
        // Asegúrate de estar obteniendo el JSON de la respuesta
        const result = jqXHR.responseText;
        console.log(result);

        if (result.success) {
            abp.notify.info(l('ProductoCreatedSuccesfully'));
            dataTable.ajax.reload();
        } else {
            if (result.errors) {
                // Mostrar todos los errores de validación
                result.errors.forEach(function (error) {
                    abp.notify.error(error);
                });
            } else {
                abp.notify.error(result.message || l('UnexpectedError'));
            }
        }
    });

    editModal.onResult(function () {
        abp.notify.info(l('ProductoEditedSuccesfully'));
        dataTable.ajax.reload();
    });

    $('#NewProductoButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });








    //Prueba para mostrar las cards

    //const firebaseConfig = {
    //    apiKey: "AIzaSyAgKENsXD_em4OZUU0z03sLh3wRnUnNDac",
    //    authDomain: "pawswhiskers-79511.firebaseapp.com",
    //    databaseURL: "https://pawswhiskers-79511-default-rtdb.firebaseio.com/",
    //    projectId: "pawswhiskers-79511",
    //    storageBucket: "pawswhiskers-79511.appspot.com",
    //    messagingSenderId: "626296275197",
    //    appId: "1:626296275197:web:9695e60ad5970608f6be94",
    //    measurementId: "G-MEL51H5EF7"
    //};

    //import { initializeApp } from "firebase/app";
    //import { getDatabase } from "firebase/database";

    //const app = initializeApp(firebaseConfig);

    //const database = getDatabase(app);









});