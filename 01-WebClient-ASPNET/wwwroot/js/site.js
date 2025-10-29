// Site-wide JavaScript

$(document).ready(function() {
    console.log('Helpdesk WebClient carregado!');

    // Auto-hide alerts após 5 segundos
    setTimeout(function() {
        $('.alert').fadeOut('slow');
    }, 5000);
});
