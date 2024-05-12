const printBtn = document.querySelector('.print-link');

printBtn.addEventListener('click', () => {
    SendForPrint();
});

function SendForPrint() {
    let GoodsId = GetGoodsIdArray();

    if (!GoodsId) GoodsId = [];

    const dataForPrint = {
        PrintViewType: GetSortTypeList(),
        GoodsId
    };

    const json = JSON.stringify(dataForPrint);

    document.getElementById('dataInput').value = json;
    document.getElementById('dataForm').submit();
}

function GetSortTypeList() {
    const linksList = document.querySelectorAll('.sort-link');
    let currentSortType;
    linksList.forEach(link => {
        if (link.classList.contains('sort-link_active')) {
            currentSortType = link.textContent;
        }
    });

    switch (currentSortType) {
        case 'Полный список':
            return 0;
        case 'По категориям':
            return 1;
        case 'По производителям':
            return 2;
    }
}
function GetGoodsIdArray() {
    if (sessionStorage.getItem('workList') === null)
        return null;
    return JSON.parse(sessionStorage.getItem('workList')).goodsIdArr;
}