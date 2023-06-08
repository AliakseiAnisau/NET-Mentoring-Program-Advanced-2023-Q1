﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.GraphQL.GraphQL;
using GraphQL.Types;
using Catalog.Application.Categorys.Queries.GetCategories;

namespace Catalog.GraphQL.GraphQL.Types;

public class CategoryType : ObjectGraphType<Category>
{
    public CategoryType()
    {
        Name = "Category";
        Description = "A category of a product";

        Field(d => d.Id, nullable: false).Description("The id of the category.");
        Field(d => d.Name, nullable: true).Description("The description of the category.");
        Field(d => d.ParentCategoryId, nullable: true).Description("The id of the parent category.");
        Field(d => d.ParentCategoryName, nullable: true).Description("The name of the parent category.");
        Field(d => d.ImageUrl, nullable: true).Description("The url of the image.");

        //Field<ListGraphType<CharacterInterface>>(
        //    "friends",
        //    resolve: context => data.GetFriends(context.Source)
        //);
        //Field<ListGraphType<EpisodeEnum>>("appearsIn", "Which movie they appear in.");
        //Field(d => d.PrimaryFunction, nullable: true).Description("The primary function of the droid.");

        //Interface<CharacterInterface>();
    }
}
