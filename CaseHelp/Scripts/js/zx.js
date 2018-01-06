function hasTouch() {
    var touchObj = {};
    touchObj.isSupportTouch = "ontouchend" in document ? true : false;
    touchObj.isEvent = touchObj.isSupportTouch ? 'touchend' : 'click';
    return touchObj.isEvent;
}
//去除html标签
function removehtml(str){
	return str.replace(/<[^>]+>/g,"");
}
//body禁止滚动
function body_no() {
    $('body').css('overflow', 'hidden');
}
//body解除禁止滚动
function body_yes() {
    $('body').css('overflow', 'auto');
}


///插件封装提示
function layeralert(cont, times) {
    var tim = 1;
    layer.open({
        content: cont, skin: 'msg', time: tim//2秒后自动关闭
    });
}


//复选框
$(document).on('click',".zx-lab",function(){
	var input=$(this).find('.zx-input');
	var gou=$(this).find('.zx-input-k');
	if(gou.hasClass('radio')){
		//单选框
		if(!input.is(':checked')){
			var na=input.attr('name');
			$("input[name="+na+"]").parents('.zx-lab').find('.zx-input-k').removeClass('hover');
			input.prop("checked", true);
			gou.addClass('hover');
		}
	}else{
		//复选
		if(input.is(':checked')){
			input.prop("checked", false);
			gou.removeClass('hover');
		}else{
			input.prop("checked", true);
			gou.addClass('hover');
		}
	}
});
//全选
$(document).on('click', '.quanxuan', function () {
    if ($(this).find('.zx-input').is(':checked')) {
        $('.zx-tab-check').find('.zx-input').prop("checked", true);
		$('.zx-tab-check').find('.zx-input-k').addClass('hover');
    } else {
        //$('.zx-tab-check').find('.zx-input').prop("checked", false);
		//$('.zx-tab-check').find('.zx-input-k').removeClass('hover');
    }
});
$(document).on('click', '.zx-tab-check', function () {
    var size = $(this).parents('body').find('.zx-tab-check').size();
	var size1=0;
    /*if($(this).attr('data-type')!=null){
		var temp=$(this).attr('data-type');
		var tempsize=0;
		$(this).parents('body').find('.zx-tab-check').each(function(){
			if($(this).attr('data-type')==temp){
				size1++;
				if($(this).is(':checked'))tempsize++;
			}
		});
		if(size1==tempsize){
			$('.zx-tab-check.check'+temp).prop('checked', true);
		}else{
			$('.zx-tab-check.check'+temp).prop('checked', false);
		}
	}*/
    if (size == $('.zx-tab-check .zx-input:checked').size()) {
        $('.quanxuan .zx-input').prop('checked', true);
		$('.quanxuan').find('.zx-input-k').addClass('hover');
    } else {
        $('.quanxuan .zx-input').prop('checked', false);
		$('.quanxuan').find('.zx-input-k').removeClass('hover');
    }
    /*if ($(this).attr('data-check') != null) {
        var he = $(this);
        $('.zx-tab-check').each(function () {
            if ($(this).attr('data-type') == he.attr('data-check')) {
                $(this).prop('checked', he.is(':checked'));
            }
        });
    }*/
})
//table切换
var zx_i=0;
var zx_scroll=0;
//tab选择
$(document).on(hasTouch(),'.zx-tab .tab-title li',function(){
	var tab = '.zx-tab';
    var tab_title = $(this).parents(".tab-title");
	var data_id="";
	if(tab_title.attr('data-id')){
		data_id=tab_title.attr('data-id');
	}
	var index=$(this).index();
	$(this).addClass('hover').siblings().removeClass('hover');
	if(data_id!="flash"){
		if(data_id==""){
		$(this).parents('.zx-tab').find('.tab-content-item'+data_id).eq(index).addClass('hover').fadeIn().siblings('.tab-content-item'+data_id).removeClass('hover').hide();
		}else{
			$('.tab-content-item'+data_id).eq(index).addClass('hover').fadeIn().siblings('.tab-content-item'+data_id).removeClass('hover').hide();
		}
		tab_end();
	}else{
		//如果是手机滑动切换
		var winwid=$(window).width();
		zx_i=index;
		zx_scroll=-index*winwid;
		$(this).parents('.zx-tab').find('.tab-flash').stop().animate({'left':-winwid*index},200);
		$(this).parents('.zx-tab').find('.tab-content-item').eq(index).addClass('hover').siblings('.tab-content-item').removeClass('hover');
		$(this).parents('.zx-tab').find('.tab-content').height($(this).parents('.zx-tab').find('.tab-content-item').eq(index).height());
		tab_end();
	}
});
//切换tab后需要调用的function
function tab_end(){}
$(function(){
	var winwid=$(window).width();
	$('.tab-content').each(function() {
        var hei=$(this).find('.tab-c.hover').height();
		var size=$(this).find('.tab-c').size();
		$(this).height(hei);
		$(this).find('.tab-c').width(winwid);
		$(this).find('.tab-flash').width(winwid*size);
    });
	tab_flash($('.tab-content .tab-flash'));
});
function tab_flash(ul){
	var winwid=$(window).width();
	var size=ul.find('.tab-c').size();
	var items=ul.find('.tab-c');
	var title=ul.parents('.zx-tab').find('.tab-title li');
	//var i=zx_i;
	var x=0;
	var y=0;
	var startX = 0;//div的初始坐标
	var scroll = zx_scroll;//拖动的距离
	ul.on('touchstart',function(e){
		y = e.originalEvent.touches[0].pageY;
		x=e.originalEvent.touches[0].pageX;
		startX = zx_scroll;
		this.style.transition = "";
	});
	ul.on('touchmove',function(e){
		var xL = 0;
		var yL=0;
		yL=e.originalEvent.changedTouches[0].pageY - y;
		xL=e.originalEvent.changedTouches[0].pageX - x;
		var w = xL<0?xL*-1:xL;     //x轴的滑动值
		var h = yL<0?yL*-1:yL;     //y轴的滑动值
		if(w>h){                //如果是在x轴中滑动
		   e.preventDefault();
		}
		xL=e.originalEvent.changedTouches[0].pageX - x;
		scroll = startX + xL;
		this.style.left = scroll +"px";
		this.style.transition = "";
	});
	ul.on('touchend',function(e){
		var xL = 0;
		xL=e.originalEvent.changedTouches[0].pageX - x;
		scroll = startX + xL;
		if(scroll>-winwid*zx_i){
			if((scroll+winwid*zx_i)>(winwid/6)){
			zx_i--;
			if(zx_i<=0){zx_i=0;}
			}
		}else{
			if(-(scroll+winwid*zx_i)>(winwid/6)){
			zx_i++;
			if(zx_i>=size-1){zx_i=size-1;}
			}
		}
		scroll=-winwid*zx_i;
		ul.stop().animate({'left':-winwid*zx_i},200);
		if(zx_i<=0){
			ul.css('left',0);
		}
		scroll=-winwid*zx_i;
		zx_scroll=scroll;
		items.eq(zx_i).addClass('hover').siblings().removeClass('hover');
		title.eq(zx_i).addClass('hover').siblings().removeClass('hover');
		ul.parents('.tab-content').height(items.eq(zx_i).height());
		tab_end();
	});
}


//普通弹窗
//取消弹窗
function zx_pop_reset(box) {
    $(box).removeClass('zx-up').addClass('zx-down').hide();
}
//关闭全部弹窗
$(document).on(hasTouch(), '.zx-box .bg,.zx-box .zx-close,.zx-nav-btn', function () {
    $(this).parents('.zx-box').removeClass('zx-up').addClass('zx-down').hide();
    var index = $(this).parents('.zx-box').attr('data-id');
    $('.zx-tab-text' + index).parents('.zx-tab-btn').trigger('click');
});
//屏幕尺寸改变时
$(window).resize(function(){
	juzhong('.zx-box-pt');
	juzhong('.zx-box-delet');
	juzhong('.zx-box-cq');
	juzhong('.zx-box-shili');
});

//居中
function juzhong(div){
	var winhei=$(window).height();
	if($(div).size()>0){
		var box_c=$(div+' .zx-box_content').height()+$(div+' .zx-box-c .box_footer').height();
		if(winhei<box_c){
			$(div+' .zx-box-c').css({'top':0});
			$(div+' .zx-box-c .zx-box_content').css({"height":winhei});
		}else{
			$(div+' .zx-box-c').css('top',(winhei-box_c)/2);
		}
	}
}
function pt_box(content,guanbi,queren,quxiao,title){
	//var box=".zx-box-pt";
	var html=typehtml(content,guanbi,queren,quxiao,title);
	//$(box).html(html);//给弹出层添加html
	//$(box).attr('data-id',type);//给弹出层付data-id
	if ($('.zx-box-pt').size()>0) {
        $('.zx-box-pt').remove();
    }
	$('body').append(html);
    juzhong('.zx-box-pt');
}
function qy_box(clas){
	//var html=$(clas).html();
	//html=qyhtml(html);
	//if ($('.zx-box-pt').size()>0) {
    //    $('.zx-box-pt').remove();
    //}
	//$('body').append(html);
    //juzhong('.zx-box-pt');
    //$("#" + clas + "title").val("");
    $(".danxuan").hide();
    $(".duoxuan").hide();
    $(".danwb").hide();
    $(".tiankong").hide();
    $(clas).show();
}
function hide_box(clas) {
    $(clas).hide();
}

//企业弹窗
function qyhtml(text){
	var html="";
    html+='<div class="zx-box-pt zx-up zx-box bg-w" style="display:block">';
    html+='<div class="zx-box-c">';
    html+='<div class="zx-box_content">';
	
    html+="<div class='nr'>"+text+"</div>";
    html+='</div>';
    html+='</div>';
    html+='<div class="bg"></div>';
    html+='</div>';
	return html;
}
//全屏滚动弹窗html为普通弹窗
function typehtml(text,guanbis,querens,quxiaos,titles){
	guanbi=0;
	queren=0;
	quxiao=0;
	title="温馨提示";
	(titles == ""|titles == null) ? title = title : title = titles;
	(guanbis == ""|guanbis == null) ? guanbi = guanbi : guanbi = guanbis;
	(querens == ""|querens == null) ? queren = queren : queren = querens;
	(quxiaos == ""|quxiaos == null) ? quxiao = quxiao : quxiao = quxiaos;
    var winhei = $(window).height();
	var html="";
    html+='<div class="zx-box-pt zx-up zx-box bg-w" style="display:block">';
    html+='<div class="zx-box-c">';
    html+='<div class="zx-box_content">';
	
    html+='<div class=\"putongneirong\">';
	html+='<div class="zx-head">';
	if(guanbi==1){
		html+='<div class="zx zx-close zx-012">&#xf029;</div>';
	}
	html+='<h3 class="fm fmc3">'+title+'</h3>';
	html+='</div>';
    html+="<div class='nr'>"+text+"</div>";
	if(quxiao==1|queren==1){
    html+='<div class="box_footer clearfix">';
	if(queren==1){
		html+="<a href='javascript:tan_submit();' class='submit'>确认</a>";
	}
	if(quxiao==1){
		html+="<a href='javascript:;' class='zx-close reset'>取消</a>";
	}
	html+='</div>';
	}
    html+='</div>';
    html+='</div>';
    html+='</div>';
    html+='<div class="bg"></div>';
    html+='</div>';
	return html;
}



