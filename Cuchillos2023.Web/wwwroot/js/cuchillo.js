var cuchilloTable;

$(document).ready(function () {

    cuchilloTable = $('#tblCuchillos').DataTable({
        "ajax": {
            "url": "/Admin/Cuchillo/GetAll"
        },
        "columns": [
            { "data": "nombreCuchillo" },
            { "data": "precio" },

            {
                "data": "id",
                "render": function (data) {
                    return `
                    
                            <a class="btn btn-warning" href="/Admin/Cuchillo/UpSert?id=${data}" >
                                <i class="bi bi-pencil-square"></i>&nbsp;
                                Editar
                            </a>
                            <a class="btn btn-danger" onclick="Delete('/Admin/Cuchillo/Delete/${data}')" >
                                <i class="bi bi-trash3"></i> &nbsp;
                                Borrar
                            </a>

                    `
                }
            }

        ]

    });

});


function Delete(url) {
    console.log(url);
    Swal.fire({
        title: '¿Estas seguro?',
        text: "¡No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '¡Sí, bórralo!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                "url": url,
                "type": 'DELETE',
                "success": function (data) {
                    console.log(data);
                    if (data.success) {
                        cuchilloTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }

            });
        }
    })
};