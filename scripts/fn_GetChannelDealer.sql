CREATE FUNCTION [RM].[fn_GetChannelDealer] (@UserId BIGINT) RETURNS TABLE AS RETURN (
  WITH [as_tree_cte] AS (
    SELECT
      0 AS cte_level,
      a.[Id],
      a.[OwnerId]
    FROM
      [RM].[Sys_UserInvite] a
    WHERE
      (a.[Id] = @UserId) UNION ALL
    SELECT
      wct1.cte_level + 1 AS cte_level,
      wct2.[Id],
      wct2.[OwnerId]
    FROM
      [as_tree_cte] wct1
      INNER JOIN [RM].[Sys_UserInvite] wct2 ON wct2.[Id] = wct1.[OwnerId]
  ) SELECT TOP
    1 a.[Id] AS ChannelDealerId
  FROM
    [as_tree_cte] a
  WHERE
    a.id <> 370942943322181
  ORDER BY
    a.cte_level DESC
)