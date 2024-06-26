﻿const rowsList = document.querySelectorAll('.goods-row');
const clearWorkListBtn = document.querySelector('.worklist-link-clean');

let rowIdList = [];

if (sessionStorage.getItem('workList') !== null) {
    sessionStorage.removeItem('workList');
}


rowsList.forEach(item => {
    item.addEventListener('click', (e) => {
        selectGoods(e);
    });

    if (rowIdList.length === 0) return;

    const rowId = item.dataset.goodsid;

    if (rowIdList.filter(id => id === rowId).length !== 0) {
        item.classList.add('goods-row-selected');
        const btn = item.querySelector('.add-delete-goods-to-list');
        const icon = btn.querySelector('i');
        icon.classList.remove('fa-plus');
        icon.classList.add('fa-trash-alt');
    }
});

clearWorkListBtn.addEventListener('click', () => {
    cleaningWorkList(rowsList, clearWorkListBtn)
});

function selectGoods(e) {
    if (!(e.target.classList.contains('item-control-add') || e.target.classList.contains('fas')))
        return;

    const goodsId = e.currentTarget.dataset.goodsid;
    const addDeleteBtn = e.currentTarget.querySelector('.add-delete-goods-to-list');
    const icon = addDeleteBtn.querySelector('i');

    if (e.currentTarget.classList.contains('goods-row-selected')) {
        if (sessionStorage.getItem('workList') !== null) {
            const workList = JSON.parse(sessionStorage.getItem('workList'));
            workList.goodsIdArr = workList.goodsIdArr.filter(item => item !== goodsId);
            if (workList.goodsIdArr.length == 0) {
                sessionStorage.removeItem('workList');
                e.currentTarget.classList.remove('goods-row-selected');
                icon.classList.remove('fa-trash-alt');
                icon.classList.add('fa-plus');
                addDeleteBtn.setAttribute('title', 'Добавить в рабочий список');
                clearWorkListBtn.classList.add('worklist-link-clean_hide');
            }
            else {
                sessionStorage.setItem('workList', JSON.stringify(workList));
                e.currentTarget.classList.remove('goods-row-selected');
                icon.classList.remove('fa-trash-alt');
                icon.classList.add('fa-plus');
                addDeleteBtn.setAttribute('title', 'Добавить в рабочий список');
            }
        }
    }
    else {
        if (sessionStorage.getItem('workList') !== null) {
            const workList = JSON.parse(sessionStorage.getItem('workList'));
            const setWorkList = new Set(workList.goodsIdArr);
            setWorkList.add(goodsId);
            workList.goodsIdArr = Array.from(setWorkList);
            sessionStorage.setItem('workList', JSON.stringify(workList));
        }
        else {
            const workList = {
                goodsIdArr: [goodsId]
            };
            sessionStorage.setItem('workList', JSON.stringify(workList));
        }

        e.currentTarget.classList.add('goods-row-selected');
        icon.classList.remove('fa-plus');
        icon.classList.add('fa-trash-alt');
        clearWorkListBtn.classList.remove('worklist-link-clean_hide');
    }
}

function cleaningWorkList(rowsList, cleanBtn) {
    rowsList.forEach(row => {
        row.classList.remove('goods-row-selected');
        const btn = row.querySelector('.add-delete-goods-to-list');
        const icon = btn.querySelector('i');
        icon.classList.add('fa-plus');
        icon.classList.remove('fa-trash-alt');
    });

    sessionStorage.removeItem('workList');

    cleanBtn.classList.add('worklist-link-clean_hide');
}