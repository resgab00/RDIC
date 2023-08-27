$(function(){
    $('#language .fa, #language .active').click(function(){
        $('#language .inactive').slideToggle();
    });
    $('#show_mobile').click(function(){
        $('#menu').slideToggle();
        if($(this).hasClass('fa-close')){
            $(this).removeClass('fa-close');
            $(this).addClass('fa-bars');
        }else{
            $(this).removeClass('fa-bars');
            $(this).addClass('fa-close');
        }
    });


    $('#menu .dropdown, #menu .dropdown .dropdown-menu').mouseover(function(){
        $(this).parent().find('.dropdown-menu').show();
    });
    $('#menu .dropdown').mouseleave(function(){
        $(this).parent().find('.dropdown-menu').hide();
    });
       
        
    if(screen.width <= 800){
        
        $('.dropdown-menu .fa-angle-right').addClass('fa-angle-down');
        $('.dropdown-menu .fa-angle-right').removeClass('fa-angle-right');

        $('#menu .dropdown-menu li').click(function(){

            $('#menu .dropdown-menu li').removeClass('active');
            $(this).addClass('active');
            $('.subdropdown-menu').slideUp();
            $(this).find('.subdropdown-menu').slideToggle();
        });

    }else{
        $('#menu .dropdown-menu li').mouseover(function(){
            $('#menu .dropdown-menu li').removeClass('active');
            $(this).addClass('active');
            $(this).find('.subdropdown-menu').show();
        });
        $('#menu .dropdown-menu li').mouseleave(function(){
            $(this).find('.subdropdown-menu').hide();
        });
    }
        
});