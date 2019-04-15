layui.use(['form', 'layedit'], function () {
    var $ = layui.$,
        form = layui.form,
        layer = layui.layer;
    var h = $(window).height();
    $("body").height(h);

    $(window).resize(function () {
        var h = $(window).height();
        $("body").height(h);
    });
    form.on('submit(login)', function (data) {
        var loadIndex = layer.load(2, {
            shade: [0.3, '#333']
        });
        let submitData = JSON.stringify(data.field);
        let showData = JSON.parse(submitData);
        $.post(
            "/Admin/Login/Checked",
            showData,
            function(obj) {
                console.log(obj);
                layer.msg(obj.msg);
                location.href = obj.url;
                layer.close(loadIndex);
            }
            , "json");
        return false;
    });
});