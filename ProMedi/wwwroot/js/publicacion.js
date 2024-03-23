var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblPublicaciones").DataTable({
        "ajax": {
            "url": "/admin/publicaciones/GetAll",
            "type": "GET",
            "datatype" : "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "20%" },
            { "data": "categoria.name", "width": "15%" },
            { "data": "fechaCreacion", "width": "%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Publicaciones/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                <i class="far fa-edit"></i>Editar
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Publicaciones/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                <i class="far fa-trash-alt"></i>Borrar
                                </a>
                            </div>
                            `;
                }, "width": "40%"
               
            }
        ]

    });
}
//metodo que se encarga de eliminar un registro a traves de ajax,
//llamando a la api de sweet alerts para mostrar un mje de alerta
//llamando a la api de toastr para mostrar un mensaje de exito
//recargando el datatable con la informacion actualizada

function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}
