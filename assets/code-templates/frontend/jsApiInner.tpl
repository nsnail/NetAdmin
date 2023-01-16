    /**
     * {0}
     */
    {1} :{{
        url: `${{config.API_URL}}/{2}`,
        name: `{0}`,
        {3}:async function(data, config={{}}) {{
            return await http.{3}(this.url,data, config)
        }}
    }},