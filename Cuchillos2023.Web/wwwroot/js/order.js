var orderTable;

$(document).ready(function () {
    var url = window.location.search;
    console.log(url);
    if (url.includes("pending")) {
        loadDataTable("pending")
    } else {
        if (url.includes("inprocess")) {
            loadDataTable("inprocess")

        } else {
            if (url.includes("completed")) {
                loadDataTable("completed")
            } else {
                if (url.includes("approved")) {
                    loadDataTable("approved")
                } else {
                    loadDataTable("all")

                }
            }
        }
    }

});

function loadDataTable(status) {
    orderTable = $('#tblTable').DataTable({
        "ajax": {
            "url": "/Admin/Order/GetAll?status=" + status
        },
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "phoneNumber" },
            { "data": "applicationUser.email" },
            { "data": "orderStatus" },
            { "data": "orderTotal" },

            {
                "data": "id",
                "render": function (data) {
                    return `
                    
                            <a class="btn btn-info" href="/Admin/Order/Details?orderId=${data}" >
                                <i class="bi bi-info-square"></i>&nbsp;
                                Detalles
                            </a>

                    `
                }
            }

        ]

    });

};
