$(document).ready(function () {
    $('#Cover').on('change', function () {
        $('.Cover-preview').attr('src', window.URL.createObjectURL(this.fiels[0])).removeClass('d-none')
    });
});
