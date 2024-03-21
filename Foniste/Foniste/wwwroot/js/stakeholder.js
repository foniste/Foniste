(function ($) {
    
    new WOW().init();
    $(document).ready(function () {
        $(".vendor-carousel").owlCarousel({
            loop: true,
            margin: 10,
            autoplay: true, // Otomatik oynatma
            autoplayTimeout: 5000, // Kaydırma aralığı (ms cinsinden)
            autoplayHoverPause: true, // Fare üzerine gelince duraklatma
            responsive: {
                0: {
                    items: 3
                },
                576: {
                    items: 5
                },
                992: {
                    items: 7
                }
            }
        });
    });
})(jQuery);