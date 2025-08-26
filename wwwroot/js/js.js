function searchGet(key) {
    let urlParams = new URLSearchParams(location.search)
    return urlParams.get(key)
}

function searchSet(key, val) {
    let urlParams = new URLSearchParams(location.search)
    urlParams.set(key, val)
    location.search = decodeURIComponent(urlParams.toString());
}
function ready(fn) {
    if (document.readyState !== 'loading')
        fn()
    else
        document.addEventListener('DOMContentLoaded', fn)
}

ready(function () {
    var filterInputs = [...document.querySelectorAll("input[type=text].filter")]
    for (let node of filterInputs) {
        node.addEventListener("keydown", function (e) {
            if (e.key === "Enter")
                searchSet("filter", filterInputs.filter(n => n.value.length > 0).map(n => `${n.getAttribute("data-col")}:${n.value}`).join('|'))
        })
    }
})

ready(function () {
    var sortLinks = [...document.querySelectorAll("a.sort-asc, a.sort-desc")]
    for (let node of sortLinks) {
        node.href = "javascript:void(0)"
        node.addEventListener("click", function (e) {
            searchSet("sort", this.getAttribute("data-sort"))
        })
    }
})

ready(function () {
    var deleteLinks = [...document.querySelectorAll("a.delete")]
    for (let node of deleteLinks) {
        node.href = "javascript:void(0)"
        node.addEventListener("click", function (e) {
            if (confirm(this.getAttribute("data-msg"))) {
                this.parentNode.querySelector('form.delete [name=sort]').value = searchGet('sort')
                this.parentNode.querySelector('form.delete [name=filter]').value = searchGet('filter')
                this.parentNode.querySelector('form.delete').submit()
            }
        })
    }
})