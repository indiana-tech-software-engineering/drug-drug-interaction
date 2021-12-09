$("#drugSearch").on("input", function ()
{
	const query = $(this).val()
	const items = $('.drug-name')
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

		$(".drug-name")
			.filter(function () { return results.includes($(this).text()) })
			.parent()
			.show()

		$(".drug-name")
			.filter(function () { return !results.includes($(this).text()) })
			.parent()
			.hide()
	}
	else
	{
		$(".drug-name")
			.parent()
			.show()
	}
})

const toSortResult = (result) => result ? 1 : 0
