//列表类封装
function HYtable(tableoption) {
    var $ = layui.$;
    var _this = this;
    var options = tableoption;
    var table;
    var tableuse;
    var tableIns;
    this.romance = function () {
        tableuse = layui.use('table', function () {
            table = layui.table;
            tableIns = table.render(options);
        });
    }
    this.getSelect = function () { //获取选中的行
        var check = [];
        var allData = _this.AllRows();
        $.each(allData, function () {
            if (this.LAY_CHECKED) {
                check.push(this);
            }
        });
        return check;
    }
    this.AllRows = function () {//获取所有行 返回数组对象
        return tableuse.table.cache[options.id];
    }
    this.load = function (data) {//通过数组对象，重新刷新列表  会清空table的url避免刷新table导致修改的数据还原为url数据
        var tmpurl = options.url;
        options.url = null;
        options.data = data;
        table.render(options);
    }
    this.reload = function () { //重载
        tableIns.reload(options);
    }
    this.getOptions = function () {//获取载入参数
        return options;
    }
    this.getTableContent = function () {//获取table上下文
        return tableIns;
    }
    this.selected = function (filed, resourse) {//选中某些行 filed判断字段 resourse需要选中行的数组对象
        if (filed && resourse && resourse.length > 0) {
            var allData = _this.AllRows();
            $.each(allData, function () {
                var datadefine = this;
                var _value = eval('(datadefine.' + filed + ')');
                for (var _m = 0; _m < resourse.length; _m++) {
                    if (_value == resourse[_m]) {
                        datadefine.LAY_CHECKED = true;
                    }
                }
            });
            _this.load(allData);
        }
    }
    this.getByIndex = function (index) {//通过索引获取数据
        if (index != undefined && index != 'undefined') {
            var allData = _this.AllRows();

            for (var _m = 0; _m < allData.length; _m++) {
                if (index == allData[_m].LAY_TABLE_INDEX) {
                    return allData[_m];
                }
            }
        }
    }
    this.selectedByIndex = function (index) {//通过索引选中一行
        if (index != undefined && index != 'undefined') {
            var allData = _this.AllRows();
            $.each(allData, function (p1, p2) {
                if (this.LAY_TABLE_INDEX == index) {
                    this.LAY_CHECKED = true;
                }
            });
            _this.load(allData);
        }
    }
    this.unselectedByIndex = function (index) {//通过索引取消选中一行
        if (index != undefined && index != 'undefined') {
            var allData = _this.AllRows();
            $.each(allData, function (p1, p2) {
                if (this.LAY_TABLE_INDEX == index) {
                    this.LAY_CHECKED = false;
                }
            });
            _this.load(allData);
        }
    }
    this.selectedAll = function () {//选中所有
        var allData = _this.AllRows();
        $.each(allData, function (p1, p2) {
            this.LAY_CHECKED = true;
        });
        _this.load(allData);
    }
    this.unselectedAll = function () {//取消选中所有
        var allData = _this.AllRows();
        $.each(allData, function (p1, p2) {
            this.LAY_CHECKED = false;
        });
        _this.load(allData);
    }
    this.updateRow = function (index, data) {//更新索引所在的行
        if (index != undefined && index != 'undefined' && data) {
            var allData = _this.AllRows();
            var loadData = [];
            $.each(allData, function () {
                if (this.LAY_TABLE_INDEX == index) {
                    loadData.push(data);
                } else {
                    loadData.push(this);
                }
            });
            _this.load(loadData);
        }
    }
    this.appendAfterRow = function (data) {//在最后面插入一行
        if (data) {
            var allData = _this.AllRows();
            if (_this.AllRows() == null) {
                allData = [data];
            } else {
                allData.push(data);
            }
            _this.load(allData);
        }
    }
    this.appendBeforeRow = function (data) {//在最前面插入一行
        if (data) {
            var allData = _this.AllRows();
            var loadData = [];
            loadData.push(data);
            $.each(allData, function () {
                loadData.push(this);
            });
            _this.load(loadData);
        }
    }
    this.insertRow = function (index, data) {//在索引后插入一行
        if (index != undefined && index != 'undefined' && data) {
            var allData = _this.AllRows();
            var loadData = [];
            $.each(allData, function () {
                loadData.push(this);
                if (this.LAY_TABLE_INDEX == index) {
                    loadData.push(data);
                }
            });
            _this.load(loadData);
        }
    }
    this.deleteRow = function (index) {//通过索引删除一行
        if (index != undefined && index != 'undefined') {
            var allData = _this.AllRows();
            var loadData = [];
            $.each(allData, function () {
                if (this.LAY_TABLE_INDEX != index) {
                    loadData.push(this);
                }
            });
            _this.load(loadData);
        }
    }

    this.deleteLoad = function () {
        var allData = _this.AllRows();
        var loadData = [];
        $.each(allData, function () {
            if (this.LAY_TABLE_INDEX != null) {
                loadData.push(this);
            }
        });
        _this.load(loadData);
    }


}



function clone(obj) {
    var o;
    if (typeof obj == "object") {
        if (obj === null) {
            o = null;
        } else {
            if (obj instanceof Array) {
                o = [];
                for (var i = 0, len = obj.length; i < len; i++) {
                    o.push(clone(obj[i]));
                }
            } else {
                o = {};
                for (var j in obj) {
                    o[j] = clone(obj[j]);
                }
            }
        }
    } else {
        o = obj;
    }
    return o;
}