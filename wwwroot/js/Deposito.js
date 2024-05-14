
let dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {


    datatable = $('#tblDatos').DataTable({

        "ajax": {
            "url": "Deposito/ObtenerTodos"
        },
        "columns": [

            { "data": "nomdepo", "width": "10%" },
            { "data": "direccion", "width": "20%" },
            { "data": "tel1", "width": "10%" },
            { "data": "email", "width": "20%" },
            { "data": "responsable", "width": "10%" },
            {
                "data": "estado",
                "render": function (data) {
                    if (data == 1) {
                        return "Activo";
                    } else {
                        return "Inactivo";
                    }
                }, "width": "10%"
            },
            {
                "data": "iddeposito",
                "render": function (data) {
                    return `
                        <div>
                            <a href="/Admin/Deposito/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                            <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/Admin/Deposito/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                            <i class="bi bi-trash3-fill"></i>
                            </a>
                        </div>
                    `;
                },"width":"20%"
               
            }
        ]
    });

}

function Delete(url) {
    swal({
        title: "Esta seguro de eliminar el deposito?",
        text: "Este recurso no se podra recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(dat.message);
                    }
                }
            });
        }
    });

}

