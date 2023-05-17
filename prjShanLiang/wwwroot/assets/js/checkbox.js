/* Function to check for any selected checkboxes and indicate
        this by highlighting the selected item's parents (in case
        the list isn't expanded to see something is selected within it)'*/
        function updateParentFacets() {

            // NOTE: It would be better if this function removed the has-selected class first, and then just found each checked input, adding a has-selected class to their iossearchfacets li parents. Rather than doing a find on each parent.

            $(".iossearchfacets li").each(function (i) {

                if ($(this).find("input:checked").length > 0) {
                    $(this).addClass("has-selected");
                }
                else {
                    $(this).removeClass("has-selected");
                }
            });
        }

        /* Go through facets and identify those that have children */
        $(".iossearchfacets li").each(function () {

            if ($(this).children("ul").length > 0) {
                $(this).addClass("has-children");
            }
        });

        /* When clicking a facet, identify whether the name or input checkbox has been clicked. If it's not the checkbox and it's an element with children -> toggle the open class. Otherwise, update the parent facets status. */
        $(".iossearchfacets li").click(function (e) {

            e.stopPropagation();

            var target = $(e.target);
            if (!target.is("input") && $(this).hasClass("has-children")) {
                $(this).toggleClass("open")
            }
            else {
                updateParentFacets();
                $(document).trigger("MyEvent:SearchFacetsUpdated");
            }
        });





        // Event listener waiting for search facets to be updated before updating the markup for removeable support (Facets shown in 'Currently selected' list.
        $(document).on("MyEvent:SearchFacetsUpdated", {

        }, function (event) {

            // clear currently selected html
            $("#removeableFacets").html("");

            // find each selected facet checkbox and append to 'currently selected' html
            $(".facetselector input:checked").each(function (i) {

                $("#removeableFacets").append("<span class='removeable-facet' data-value='" + $(this).val() + "'>" + $(this).parent(".facetselector").text() + "</span>")
            });

            /* On click of a removeable facet, we just simulate a click
               */
            $(".removeable-facet").click(function () {

                var facetValue = $(this).data("value");
                $(".facetselector input[value='" + facetValue + "']").click();
            });

        });