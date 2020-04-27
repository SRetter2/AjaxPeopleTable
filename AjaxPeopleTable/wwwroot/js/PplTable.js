$(() => {

    function LoadPeople() {
        $("#ppl-table tbody td").remove();
        $.get('/home/getPeople', ppl => {           
            ppl.forEach(p => {
                $("#ppl-table tbody").append
                    (`<tr data-first-name="${p.firstName}" data-last-name="${p.lastName}" data-age="${p.age}">
                        <td >${p.firstName}</td>
                        <td >${p.lastName}</td>
                        <td >${p.age}</td>
                        <td> <button class="btn btn-primary" data-id="${p.id}"> Edit  </button></td>
                        <td> <button class="btn btn-danger" data-id="${p.id}"> Delete </button></td>
                    </tr>`);
            });
        });
    };

    LoadPeople();

    $("#add-btn").on('click', () => {
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();
        const person = {
            firstName,
            lastName,
            age
        };
        $.post(`/home/addperson`, person, function(person) {
            LoadPeople();
        });
        $("#first-name").val('');
        $("#last-name").val('');
        $("#age").val('');
    });
    $("#ppl-table").on('click', ".btn-danger", function () {
        const id = $(this).data('id');
        console.log(id);
        $.post(`home/deleteperson?id=${id}`, function () {
            LoadPeople();
        });
    });
    $("#ppl-table").on('click', ".btn-primary", function () {
        const id = $(this).data('id');
        const firstName = $(this).closest("tr").data('first-name');
        const lastName = $(this).closest("tr").data('last-name');
        const age = $(this).closest("tr").data('age');
        $("#edit-name").text(`Edit ${firstName} ${lastName}`);
        $("#first-name-modal").val(firstName);
        $("#last-name-modal").val(lastName);
        $("#age-modal").val(age);
        $("#edit-modal").modal();
        $("#save").on('click', function () {
            const firstName = $("#first-name-modal").val();
            const lastName = $("#last-name-modal").val();
            const age = $("#age-modal").val();
            $("#edit-modal").modal('hide');
            $("#edit-name").text('');
            const person = {
                id,
                firstName,
                lastName,
                age
            };
            $.post(`/home/editPerson`, person, function () {
                LoadPeople();
            });
        });        
    });
});
