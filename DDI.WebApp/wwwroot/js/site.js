const drugNameCell = '.drug-name'
const searchInput = '#drugSearch'
const dataTable = '#dataTable'

$(searchInput).on("input", function ()
{
	const query = $(this).val()
	const items = $(drugNameCell)
		.toArray()
		.map(x => x.innerText)

	const fuse = new Fuse(items, {
		includeScore: true,
	})

	if (0 < query?.length ?? 0)
	{
		const matchingItems = fuse.search(query)
		const results = matchingItems.map(x => x.item)
		const scores = matchingItems
			.map(({ item, score }) => ({ [item]: score }))
			.reduce((x, y) => ({ ...x, ...y }), {})

		const getScore = item => scores[item] ?? -1

		$(drugNameCell)
			.filter(function () { return results.includes($(this).text()) })
			.parent()
			.show()

		$(drugNameCell)
			.filter(function () { return !results.includes($(this).text()) })
			.parent()
			.hide()

		$(dataTable)
			.find('.drug')
			.sort(function (a, b) { return getScore($(drugNameCell, a).text()) > getScore($(drugNameCell, b).text()) ? 1 : -1 })
			.appendTo($(dataTable))
	}
	else
	{
		$(drugNameCell)
			.parent()
			.show()

		$(dataTable)
			.find('.drug')
			.sort(function (a, b) { return $(drugNameCell, a).text().localeCompare($(drugNameCell, b).text()) })
			.appendTo($(dataTable))
	}
})

const toSortResult = (result) => result ? 1 : 0
