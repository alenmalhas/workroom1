import { ADD_ARTICLE } from "../constants/action-types"

const initialState = {
    articles : []
};

function rootReducer(state = initialState, action) {

    if(action.type == ADD_ARTICLE){
        var newState = Object.assign({}, state, {
            articles: stete.articles.concat(action.payload)
        });
        //state.articles.push(action.payload);
        return newState;
    }

    return state;
};

export default rootReducer;
