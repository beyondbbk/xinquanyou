function GetType() {
    weui.picker([
        {
            label: '地面观测数据-气温',
            value: 0,
            disabled: true // 不可用
        },
        {
            label: '小时最高气温',
            value: 1
        },
        {
            label: '小时气温',
            value: 3
        },
        {
            label: '小时最低气温',
            value: 4,
        },
        {
            label: '地面观测数据-降水',
            value: 5,
        },
        {
            label: '1小时降水',
            value: 6,
        }
    ], {
        className: 'custom-classname',
        container: 'body',
        defaultValue: [3],
        onChange: function (result) {
            console.log(result)
        },
        onConfirm: function (result) {
            console.log(result)
        },
        id: 'singleLinePicker'
    });
}