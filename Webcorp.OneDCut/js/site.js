(function ($) {
    'use strict';

    var MutationObserver = window.MutationObserver || window.WebKitMutationObserver || window.MozMutationObserver;

    $.fn.attrchange = function (callback) {
        if (MutationObserver) {
            var options = {
                subtree: false,
                attributes: true
            };

            var observer = new MutationObserver(function (mutations) {
                mutations.forEach(function (e) {
                    callback.call(e.target, e.attributeName);
                });
            });

            return this.each(function () {
                observer.observe(this, options);
            });

        }
    }

    /*$.fn.bootbreak = function () {
        var self = this;
        self.breakpoints = {
            'xs': $('<div class="device-xs visible-xs visible-xs-block"></div>'),
            'sm': $('<div class="device-sm visible-sm visible-sm-block"></div>'),
            'md': $('<div class="device-md visible-md visible-md-block"></div>'),
            'lg': $('<div class="device-lg visible-lg visible-lg-block"></div>')
        };

        $(document).ready(function () {
            $('<div class="responsive-bootstrap-toolkit"></div>').appendTo('body');
            $.each(self.breakpoints, function (point) {

                self.breakpoints[point].appendTo('.responsive-bootstrap-toolkit');
            });
            $('.responsive-bootstrap-toolkit *').attrchange(function (attr) {
                alert(attr);
            });
            

        });
        
    }*/
})(jQuery);

(function ($, document, window, viewport) {
    $.fn.bootbreak = function () {

        var init = false;

        var breakpoints = ['xs', 'sm', 'md', 'lg'];

        var sendTrigger = function (className) {
            $('.responsive-bootstrap-toolkit').attr('breakpoint', className)
            $(document).trigger("breakpoint", className);
        };

        var checkBreakpoint = function (bp) {
            var current = $('.responsive-bootstrap-toolkit').attr('breakpoint');
            if (viewport.is(bp) && (current != bp || init)) {
                sendTrigger(bp);
            }
        };

        $(document).ready(function () {
            init = true;
            $('.responsive-bootstrap-toolkit').attr('breakpoint',viewport.current())
            $.map(breakpoints, checkBreakpoint);

            //console.log('Current breakpoint:', viewport.current());
            init = false;

        });

        $(window).resize(
            viewport.changed(function () {
                $.map(breakpoints, checkBreakpoint);

                //console.log('Current breakpoint:', viewport.current());
            })
        );
    };

})(jQuery, document, window, ResponsiveBootstrapToolkit);