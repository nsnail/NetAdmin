//~/**
//~ * $actionDesc$
//~ */
//~$actionName$ :{
//~    url: `${config.API_URL}/$actionPath$`,
//~        name: `$actionDesc$`,
//~        $actionMethod$:async function(data, config={}) {
//~        return await http.$actionMethod$(this.url,data, config)
//~    }
//~},