/**
 * 该方法用于给需要消息需要加载的页面进行id获取，加载指定数据
 */
var id;
var applytype;
window.onload = function () {
    //该id获取 从消息提醒传来的需要显示的指定数据
    id = window.parent.document.getElementById("txtDataId").value; //获取
    window.parent.document.getElementById("txtDataId").value = ""; //清空
    applytype = window.parent.document.getElementById("txtApplyType").value; //获取
    window.parent.document.getElementById("txtApplyType").value = ""; //清空
};
//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) {
        //return unescape(r[2]);
        return decodeURI(r[2]);
    }
    return null; //返回参数值
}

//验证身份证号并获取籍贯
function getProvinceNameByIdNo(idcard) {
    var area = {
        11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古",
        21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏",
        33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北",
        43: "湖南", 44: "广东", 45: "广西",
        46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西",
        62: "甘肃", 63: "青海", 64: "宁夏",
        65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外"
    }

    var provinceName = "";
    var provinceNo = idcard.substr(0, 2);
    if (area[parseInt(provinceNo)] != null) {
        provinceName = area[parseInt(provinceNo)];
    }
    return provinceName;
}

//获取出生日期
function getBirthdatByIdNo(iIdNo) {
    var tmpStr = "";

    if (iIdNo.length == 15) {
        tmpStr = iIdNo.substring(6, 12);
        tmpStr = "19" + tmpStr;
        tmpStr = tmpStr.substring(0, 4) + "-" + tmpStr.substring(4, 6) + "-" + tmpStr.substring(6)
        return tmpStr;
    }
    else {
        tmpStr = iIdNo.substring(6, 14);
        tmpStr = tmpStr.substring(0, 4) + "-" + tmpStr.substring(4, 6) + "-" + tmpStr.substring(6)
        return tmpStr;
    }
}

//获取性别
function getSexByIdNo(iIdNo) {
    if (parseInt(iIdNo.substr(16, 1)) % 2 == 1) {
        return "男";
    } else {
        return "女";
    }
}