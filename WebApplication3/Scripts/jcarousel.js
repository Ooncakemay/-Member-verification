(function ($) {
    $(function () {
        BannerJcarousel();
        ProductJcarousel();
        FurtherJcarousel();
        GiftJcarousel();
    });
})(jQuery);

function ProductJcarousel() {
    var jcarousel = $('.jcarousel_product');

    jcarousel
        .on('jcarousel:reload jcarousel:create', function () {
            var carousel = $(this),
                width = carousel.outerWidth(true),
                list = carousel.jcarousel('list'),
                items = carousel.jcarousel('items'),
                windowsize = $(window).width();
            //減20是因為li外面有一層margin:10px
            list.css('width', Math.ceil(width * items.length) + 'px');
            if (windowsize > 1200) {
                items.css('width', Math.ceil(width / 4) + 'px');
            } else if (windowsize > 1024 && windowsize <= 1200) {
                items.css('width', Math.ceil(width / 3) + 'px');
            } else if (windowsize > 700 && windowsize <= 1024) {				
				items.css('width', Math.ceil(width / 3)-0.5 + 'px');
            }  else if (windowsize > 600 && windowsize <= 700) {				
				items.css('width', Math.ceil(width / 2)-0.5 + 'px');
            } 
			else {
                items.css('width', Math.ceil(width) + 'px');
            }
        })
        .jcarousel({
            // Configuration goes here
            list: '.lrp_product_list'
        });

    $('.lrp_btn_com_prev_product')
        .on('jcarouselcontrol:inactive', function () {
            $(this).hide();
        })
        .on('jcarouselcontrol:active', function () {
            $(this).show();
        }).jcarouselControl({
            target: '-=1'
        });

    $('.lrp_btn_com_next_product')
        .on('jcarouselcontrol:inactive', function () {
            $(this).hide();
        })
        .on('jcarouselcontrol:active', function () {
            $(this).show();
        }).jcarouselControl({
            target: '+=1'
        });
    }
function FurtherJcarousel() {
        var jcarousel = $('.jcarousel_further');

        jcarousel
        .on('jcarousel:reload jcarousel:create', function () {
            var carousel = $(this),
                width = carousel.outerWidth(true),
                list = carousel.jcarousel('list'),
                items = carousel.jcarousel('items'),
                windowsize = $(window).width();

            //減20是因為li外面有一層margin:10px
            list.css('width', Math.ceil(width * items.length) + 'px');
            if (windowsize > 1200) {
                items.width(width / 4 - 20);
            } else if (windowsize > 768 && windowsize <= 1200) {
                items.width(width / 3 - 20);
            } else if (windowsize > 600 && windowsize <= 768) {
                items.width(width / 2 - 20);
            } else {
                items.width(width - 20);
            }
        })
        .jcarousel({
            // Configuration goes here
            list: '.lrp_article_list'
        });

        $('.lrp_btn_com_prev_further')
        .on('jcarouselcontrol:inactive', function () {
            $(this).hide();
        })
        .on('jcarouselcontrol:active', function () {
            $(this).show();
        }).jcarouselControl({
            target: '-=1'
        });

        $('.lrp_btn_com_next_further')
        .on('jcarouselcontrol:inactive', function () {
            $(this).hide();
        })
        .on('jcarouselcontrol:active', function () {
            $(this).show();
        }).jcarouselControl({
            target: '+=1'
        });
    }
    function BannerJcarousel() {
        var jcarousel = $('.jcarousel_banner');

        jcarousel
        .on('jcarousel:reload jcarousel:create', function () {
            // debugger;
            var carousel = $(this),
                width = carousel.outerWidth(true),
                list = carousel.jcarousel('list'),
                items = carousel.jcarousel('items'),
                windowsize = $(window).width();

            if (items.length == 1) {
                $('.btn_main_pre_banner').hide();
                $('.btn_main_next_banner').hide();
            }

            items.css('width', Math.ceil(width) + 'px');
            list.css('width', Math.ceil(width * items.length) + 'px');
        })
        .jcarousel({
            // Configuration goes here
            list: '.lrp_bn_kv',
            wrap: 'circular'
        });

        $('.lrp_btn_com_prev_banner')
            .jcarouselControl({
                target: '-=1'
            });

        $('.lrp_btn_com_next_banner')
            .jcarouselControl({
                target: '+=1'
            });

        $('.jcarousel_pagination_banner')
            .on('jcarouselpagination:active', 'a', function () {
                $(this).addClass('active');
            })
            .on('jcarouselpagination:inactive', 'a', function () {
                $(this).removeClass('active');
            })
            .on('click', function (e) {
                e.preventDefault();
            })
            .jcarouselPagination({
                perPage: 1,
                item: function (page) {
                    return '<a href="#' + page + '">' + page + '</a>';
                }
            });
    }
function GiftJcarousel() {
        var jcarousel = $('.jcarousel_gift');

        jcarousel
        .on('jcarousel:reload jcarousel:create', function () {
            var carousel = $(this),
                width = carousel.outerWidth(true),
                list = carousel.jcarousel('list'),
                items = carousel.jcarousel('items'),
                windowsize = $(window).width();
            list.css('width', Math.ceil(width * items.length) + 'px');
            if (windowsize > 1024) {
                items.css('width', Math.ceil(width / 4)-20 + 'px');
            } else if (windowsize > 768 && windowsize <= 1024) {
                items.css('width', Math.ceil(width / 3) - 20 + 'px');
            } else if (windowsize > 480 && windowsize <= 768) {
                items.css('width', Math.ceil(width / 2) - 20 + 'px');
            } else {
                items.css('width', Math.ceil(width) - 20 + 'px');
            }
        })
        .jcarousel({
            // Configuration goes here
            list: '.lrp_gift_list'
        });

        $('.lrp_btn_com_prev_gift')
        .on('jcarouselcontrol:inactive', function () {
            $(this).hide();
        })
        .on('jcarouselcontrol:active', function () {
            $(this).show();
        }).jcarouselControl({
            target: '-=1'
        });

        $('.lrp_btn_com_next_gift')
        .on('jcarouselcontrol:inactive', function () {
            $(this).hide();
        })
        .on('jcarouselcontrol:active', function () {
            $(this).show();
        }).jcarouselControl({
            target: '+=1'
        });
    }