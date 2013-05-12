var createAutoComplete = function () {
    var $input = $(this);
    $input.autocomplete({
        source: function (request, response) {
            $.getJSON($input.data('autocompletesource') + "?term=" + request.term, function (data) {
                response(data);
            });
        },
        select: submitAutoCompleteForm,
        minLength: 2
    });
};

function submitAutoCompleteForm(event, ui) {
    event.preventDefault();
    var $input = $(this); //input text
    $input.val(ui.item.label);
    

}


var createDatePicker = function () {
    var $input = $(this);
    $input.datepicker({ minDate: '0' });
}


function filterResults(searchTerm) {
    var option = {
        url: '/Home/FilterResult/' + searchTerm,
        data:{searchTerm:searchTerm}
    };
    $.ajax(option).done(function (data) {
        $('#searchResults').replaceWith(data);
    });
}
