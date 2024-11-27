/**
 * @file holds all current valid category for accessing later
 * @author Jenna Boyes
 */

const res = await fetch('https://opentdb.com/api_category.php');
const data = await res.json();
const categories = data.trivia_categories;
//[ { id: 9, name: 'General Knowledge' }, {...} ]

const categoryIds = [];
const categoryNames = [];

Object.values(categories).forEach((category) => {
  categoryIds.push(category.id);
  categoryNames.push(category.name);
});

export { categoryIds, categoryNames };
