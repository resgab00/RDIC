$(function(){
    
    $('.menu .dropdown span').click(function(){
        $(this).parent().find('.dropdown-menu').slideToggle();
        
        if($(this).find('.fa').hasClass('fa-angle-down')){
            $(this).find('.fa').removeClass('fa-angle-down').addClass('fa-angle-up');
        }else{
            $(this).find('.fa').removeClass('fa-angle-up').addClass('fa-angle-down');
        }
    });
    
});